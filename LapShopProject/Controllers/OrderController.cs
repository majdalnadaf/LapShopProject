using DataAccess.DaModels.Interfaces;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using LapShopProject.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DataAccess.Identity;
using Domains.Models;
using DataAccess.ApisCalling;
using BusinessLogic.Bl;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using DataAccess.DaModels;
using LapShopProject.Filters;


namespace LapShopProject.Controllers
{
    [CustomAuthorize]
    public class OrderController : Controller
    {

        #region Ctor

        private readonly IVwItem clsVwItem;
        private readonly IItem clsItem;
        private readonly IItemImage clsItemImage;
        private readonly IItemDiscount clsItemDiscount;
        private readonly ISalesInvoice clsSalesInvoice;
        private readonly ICountry clsCountry;
        private readonly IDelivery clsDelivery;
        private readonly ICashTransaction clsCashTransaction;
        private readonly IDeliveryInfo clsDeliveryInfo;
        private readonly ISalesInvoiceItems clsSalesInvoiceItem;

        SignInManager<ApplicationUser> _signInManager;
        UserManager<ApplicationUser> _userManager;

        private readonly DbLapShopContext context;
        public OrderController(IVwItem oVwItem, IItem oItem, IItemImage oItemImage, IItemDiscount oItemDiscount, DbLapShopContext ctx, SignInManager<ApplicationUser> signInManager
            , UserManager<ApplicationUser> userManager, ISalesInvoice oSalesInvoice, ICountry oCountry , IDelivery oDelivery , ICashTransaction oCashTransaction , IDeliveryInfo oDeliveryInfo
             , ISalesInvoiceItems oSalesInvoiceItem)
        {
            clsVwItem = oVwItem;
            clsItem = oItem;
            context = ctx;
            clsItemImage = oItemImage;
            clsItemDiscount = oItemDiscount;
            clsSalesInvoice = oSalesInvoice;
            _signInManager = signInManager;
            _userManager = userManager;
            clsCountry = oCountry;
            clsDelivery = oDelivery;
            clsCashTransaction = oCashTransaction;
            clsDeliveryInfo = oDeliveryInfo;
            clsSalesInvoiceItem = oSalesInvoiceItem;
        }
        #endregion

        #region OrderSuccess
        /// <summary>
        /// Save the order of customer in database and call Order Success view
        /// </summary>
        /// <param name="checkOut"></param>
        /// <returns></returns>
        public async Task<IActionResult> OrderSuccess(VwCheckout checkOut)
        {
            if (!ModelState.IsValid)
            {
                return View("CheckOut", checkOut);
            }


            // Divide the view model checkOut and Save "SalesInvoice , SalesInvoicItems , DeliveryInfo " in database
            int nInvoiceId = await SaveCheckOut(checkOut);


            // method for save cashTransaction and return CashTransactionId
            int nCashTransactionId = 0;






            // Get the salesInvoice after save it in database 
            TbSalesInvoice salesInovice = clsSalesInvoice.GetById(nInvoiceId);




            // Get the SalesInvoiceItems from database

            var lstSalesInvoiceItems = clsSalesInvoiceItem.GetById(nInvoiceId);

            // Calculate the Total Pirce of all item of salesInvoice
            decimal subTotalPrice = lstSalesInvoiceItems.Sum(a => a.InvoicePrice);




            //Get the TbCountry  of order  from database 
            var country = await clsCountry.GetById(checkOut.CountryId);



            // Get the TbDeliveryInfo of The current salesInovice
            var oDeliveryInfo = (await clsDeliveryInfo.GetAll()).Where(a => a.SalesInvoiceId == nInvoiceId).FirstOrDefault();
            if (oDeliveryInfo == null)
                oDeliveryInfo = new TbDeliveryInfo();



            // Access the user that login now
            var user = new ApplicationUser();
            if (User.Identity!=null && User.Identity.IsAuthenticated)
                user = await _signInManager.UserManager.GetUserAsync(User);

            // build the "VwOrderSuccess" view model to send it to view 
            var oVwOrderSuccess = new VwOrderSuccess();
            oVwOrderSuccess.SalesInvoice = salesInovice;
            oVwOrderSuccess.CashTransaction = await clsCashTransaction.GetById(nCashTransactionId);
            oVwOrderSuccess.DeliveryInfo = oDeliveryInfo;
            oVwOrderSuccess.SubTotalPrice = subTotalPrice;
            oVwOrderSuccess.ShippingCost = oDeliveryInfo.TbCountry.CostDelivery;
            oVwOrderSuccess.TotalPrice = subTotalPrice + oDeliveryInfo.TbCountry.CostDelivery;
            oVwOrderSuccess.User = user;
            oVwOrderSuccess.lstSalesInoivceItems = lstSalesInvoiceItems;



            return View(oVwOrderSuccess);
        } 
        #endregion


        #region Checkout

        /// <summary>
        /// Save order of customer in database
        /// </summary>
        /// <param name="checkOut"></param>
        /// <returns></returns>
        async Task<int> SaveCheckOut(VwCheckout checkOut)
        {

            var oSalesInoivce = new TbSalesInvoice();
            var lstSalesInvoiceItems = new List<TbSalesInvoiceItem>();
            var oDelieryInfo = new TbDeliveryInfo();

            var oUser = new ApplicationUser();

            if (User.Identity !=null && User.Identity.IsAuthenticated)
                oUser = await _signInManager.UserManager.GetUserAsync(User);


            var oShoppingCart = new ShoppingCart();
            if (HttpContext.Request.Cookies["Cart"] != null)
            {
                oShoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            }


            // Start : Save SalesInvoice and SalesinvoiceItems

            if (oShoppingCart != null && oShoppingCart.lstItem != null)
                foreach (var item in oShoppingCart.lstItem)
                {
                    lstSalesInvoiceItems.Add(new TbSalesInvoiceItem
                    {
                        ItemId = item.ItemId,
                        Qty = item.Qty,
                        InvoicePrice = item.Price,

                    });
                }

            var oBlDelviery = new BlDelivery(clsDelivery);
            int nDeliveryId = await oBlDelviery.GetDeliveryId();

            var oDeliveryInfo = new TbDeliveryInfo
            {
                DeliveryId = nDeliveryId,
                CountryId = checkOut.CountryId,
                State = checkOut.State,
                StreetName = checkOut.StreetAddress,
                City = checkOut.City,
                PostalCode = checkOut.PostalCode

            };



            return await clsSalesInvoice.Save(oSalesInoivce, lstSalesInvoiceItems, oDeliveryInfo, oUser, true);

            // End : Save SalesInvoice and SalesinvoiceItems



        }


        /// <summary>
        /// Display CheckOut view 
        /// </summary>
        /// <returns> CheckOut View </returns>
        public async Task<IActionResult> CheckOut()
        {

            ShoppingCart oShoppingCart = new ShoppingCart();
            if (HttpContext.Request.Cookies["Cart"] != null)
            {
                oShoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            }


            VwCheckout checkout = new VwCheckout();

            ViewBag.Countries = await clsCountry.GetAll();
            ViewBag.ShoppinCart = oShoppingCart;
            if (User.Identity.IsAuthenticated)
                ViewBag.User = await _signInManager.UserManager.GetUserAsync(User);
            else
                ViewBag.User = new ApplicationUser();

            return View(checkout);
        }
        #endregion


        #region Hash method
        //async Task SaveOrder(ShoppingCart oShoppingCart)
        //{
        //    try
        //    {
        //        List<TbSalesInvoiceItem> lstSalesInvoiceItems = new List<TbSalesInvoiceItem>();

        //        foreach (var item in oShoppingCart.lstItem)
        //        {
        //            lstSalesInvoiceItems.Add(new TbSalesInvoiceItem
        //            {
        //                ItemId = item.ItemId,
        //                Qty = item.Qty,
        //                InvoicePrice = item.Price,

        //            });
        //        }


        //        TbSalesInvoice oSalesInvoice = new TbSalesInvoice();


        //        // await clsSalesInvoice.Save(oSalesInvoice,lstSalesInvoiceItems, true);


        //    }
        //    catch(Exception e) 
        //    {

        //    }



        //} 
        #endregion

        #region Cart
        public IActionResult Cart()
        {

            ShoppingCart cart = new ShoppingCart();

            var jsonCart = HttpContext.Request.Cookies["Cart"];
            if (!string.IsNullOrEmpty(jsonCart))
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(jsonCart);
            }

            cart.PromoCode = "dff"; // Get the promoCode from TbSetting 
            return View(cart);
        }

        /// <summary>
        /// Get item by id and Save it in "Cart" cookie
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns> Redirect to Cart action </returns>
        public async Task<IActionResult> AddToCart(int itemId)
        {

            ShoppingCart cart = new ShoppingCart();

            var jsonCart = HttpContext.Request.Cookies["Cart"];
            if (!string.IsNullOrEmpty(jsonCart))
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(jsonCart);
            }

            var item = await clsItem.GetById(itemId);
            var itemInList = cart.lstItem.Find(a => a.ItemId == itemId);
            if (itemInList != null)
            {
                itemInList.Qty++;
                itemInList.TotalPrice = ((decimal)itemInList.Qty * itemInList.Price);
            }
            else
            {
                cart.lstItem.Add(new ShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ImageName = item.ImageName,
                    Qty = 1,
                    Price = item.SalesPrice,
                    TotalPrice = item.SalesPrice

                });
            }

            cart.TotalPriceOfAllItems = cart.lstItem.Sum(a => a.TotalPrice);

            jsonCart = JsonConvert.SerializeObject(cart);
            HttpContext.Response.Cookies.Append("Cart", jsonCart);


            return RedirectToAction("Cart");
        }
        #endregion


        #region MyOrders
        public async Task<IActionResult> MyOrders()
        {
            List<VwMyOrder> lstMyOrders = new List<VwMyOrder>();
            // Access the current user 
            var oUser = await _signInManager.UserManager.GetUserAsync(User);
            if(oUser!=null)
            //Get all orders of user
           await GetAllOrdersOfUser(lstMyOrders, (oUser.Id).ToString());


            return View(lstMyOrders);
        }

        async Task GetAllOrdersOfUser(List<VwMyOrder> lstMyOrders, string userId)
        {

            var lstSalesInoviceOfUser = clsSalesInvoice.GetAllByUserId(userId);
            var oVwMyOrder = new VwMyOrder();


            for (int i = 0; i < lstSalesInoviceOfUser.Count; i++)
            {
                // Fill the information of sales invoice of user in the view model 'VwMyOrder' 
                var salesInovie = lstSalesInoviceOfUser[i];
                var lstItems = clsSalesInvoiceItem.GetById(salesInovie.InvoiceId);
                oVwMyOrder = new VwMyOrder();


                foreach(var item in lstItems)
                {
                    oVwMyOrder.lstItems.Add(item.Item);

                }
                // Get the order date
                oVwMyOrder.OrderDate = salesInovie.CreatedDate;
                //Get order id 
                oVwMyOrder.OrderId = salesInovie.InvoiceId;

                // Get the country id from delivery info
                var nCountryId = salesInovie.TbDelivryInfo.CountryId;

                // Get country by id
                var oCountry =await clsCountry.GetById(nCountryId);

                
                oVwMyOrder.CountryName = oCountry.CountryName;
                oVwMyOrder.StreetName = salesInovie.TbDelivryInfo.StreetName;
                oVwMyOrder.City = salesInovie.TbDelivryInfo.City;

                lstMyOrders.Add(oVwMyOrder);
            }

        } 
        #endregion

    }
}
