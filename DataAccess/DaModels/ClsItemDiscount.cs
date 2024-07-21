using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsItemDiscount : IItemDiscount
    {

        public ClsItemDiscount(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;
        public bool Delete(int discountId)
        {
            try
            {
               var model = GetById(discountId);
                if (model != null) 
                {
                    context.Remove(model);
                    return true;
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        public List<TbItemDiscount> GetAll(int itemId)
        {
            try
            {
                var lstItemDiscount = context.TbItemDiscounts.Where(a => a.ItemId == itemId).ToList();
                if (lstItemDiscount == null)
                    return new List<TbItemDiscount>();

                return lstItemDiscount;

            }
            catch 
            {
                return new List<TbItemDiscount>();

            }
        }

        public List<TbItemDiscount> GetAll()
        {
            try
            {
                var lstItemDiscount = context.TbItemDiscounts.ToList();
                if (lstItemDiscount == null)
                    return new List<TbItemDiscount>();

                return lstItemDiscount;

            }
            catch
            {
                return new List<TbItemDiscount>();

            }
        }

        public TbItemDiscount GetById(int discountId)
        {
            try
            {
                var model = context.TbItemDiscounts.Where(a => a.ItemDiscountId == discountId).FirstOrDefault();
                if (model == null)
                    return new TbItemDiscount();

                return model;

            }
            catch
            {
                return new TbItemDiscount();


            }
        }

        public bool Save(TbItemDiscount model)
        {
            try
            {
                if (model.ItemDiscountId == 0)
                {
                    // implement properties
                    context.TbItemDiscounts.Add(model);


                }
                else 
                {
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                context.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
