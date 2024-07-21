using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsDeliveryInfo : IDeliveryInfo
    {
        public ClsDeliveryInfo(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public async Task<bool> Delete(int id)
        {
            try
            {
                var model = context.TbDeliveriesInfo.Where(a => a.DeliveryInfoId == id).FirstOrDefault();
                if (model != null)
                {

                    context.TbDeliveriesInfo.Remove(model);

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TbDeliveryInfo>> GetAll()
        {
            try
            {
                var lstDeliveryInfo = await Task.FromResult(context.TbDeliveriesInfo.Include(a => a.TbCountry).ToList());
                return lstDeliveryInfo;
            }
            catch
            {
                return new List<TbDeliveryInfo>();

            }

        }

        public async Task<TbDeliveryInfo> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbDeliveryInfo();

                var deliveryInfo = await Task.FromResult(context.TbDeliveriesInfo.Include(a => a.TbCountry).Where(a => a.DeliveryInfoId == id).FirstOrDefault());
                if (deliveryInfo != null)
                    return deliveryInfo;

                return new TbDeliveryInfo();
            }
            catch
            {
                return new TbDeliveryInfo();
            }
        }

        public async Task<bool> Save(TbDeliveryInfo model)
        {
            try
            {
                if (model.DeliveryInfoId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbDeliveriesInfo.Add(model));
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


        public  bool SaveWithoutAsync(TbDeliveryInfo model)
        {
            try
            {
                if (model.DeliveryInfoId == 0)
                {
                    // function for implemmentation feilds
                    context.TbDeliveriesInfo.Add(model);
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

