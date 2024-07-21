using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DaModels.Interfaces;
using Domains.Models;


namespace DataAccess.DaModels
{
    public class ClsDelivery : IDelivery
    {

        public ClsDelivery(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public async Task<bool> Delete(int id)
        {
            try
            {
                var model = context.TbDeliveries.Where(a => a.DeliveryId == id).FirstOrDefault();
                if (model != null)
                {
                     context.TbDeliveries.Remove(model);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TbDelivery>> GetAll()
        {
            try
            {
                var lstDelivery = await Task.FromResult(context.TbDeliveries.ToList());
                return lstDelivery;
            }
            catch
            {
                return new List<TbDelivery>();

            }

        }

        public async Task<TbDelivery> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbDelivery();

                var delivery = await Task.FromResult(context.TbDeliveries.Where(a => a.DeliveryId == id).FirstOrDefault());
                if (delivery != null)
                    return delivery;

                return new TbDelivery();
            }
            catch
            {
                return new TbDelivery();
            }
        }

        public async Task<bool> Save(TbDelivery model)
        {
            try
            {
                if (model.DeliveryId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbDeliveries.Add(model));
                }
                else
                {
                    await Task.Run(() => context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified);
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
