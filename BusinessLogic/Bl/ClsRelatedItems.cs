using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Domains.Models.VwAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Bl
{
    public class ClsRelatedItems
    {
        public static List<VwItem> GetFreeDeliveryItems(IVwItem clsVwItem , VwItem item)
        {
            try
            {
                // For this business , free delivery items are items have low rating
                var lstItems = clsVwItem.GetAll().Where(a => (a.SalesPrice >= (item.SalesPrice - 150)) && (a.SalesPrice <= (item.SalesPrice + 150))).ToList();
                if (lstItems == null)
                    return new List<VwItem>();
                
                return lstItems;
            }
            catch(Exception ex)
            {
               
                throw new Exception(ex.Message);
            }



        }
    }
}
