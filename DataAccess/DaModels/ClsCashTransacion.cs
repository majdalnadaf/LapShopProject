using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsCashTransacion : ICashTransaction
    {

        public ClsCashTransacion(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public async Task<bool> Delete(int id)
        {
            try
            {
                var model = context.TbCashTransacions.Where(a => a.CashTransactionId == id).FirstOrDefault();
                if (model != null)
                {
                    context.TbCashTransacions.Remove(model);
                    await Save(model);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TbCashTransacion>> GetAll()
        {
            try
            {
                var lstCashTransaction = await Task.FromResult(context.TbCashTransacions.ToList());
                return lstCashTransaction;
            }
            catch
            {
                return new List<TbCashTransacion>();

            }

        }

        public async Task<TbCashTransacion> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbCashTransacion();

                var cashTransaction = await Task.FromResult(context.TbCashTransacions.Where(a => a.CashTransactionId == id ).FirstOrDefault());
                if (cashTransaction != null)
                    return cashTransaction;

                return new TbCashTransacion();
            }
            catch
            {
                return new TbCashTransacion();
            }
        }

        public async Task<bool> Save(TbCashTransacion model)
        {
            try
            {
                if (model.CashTransactionId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbCashTransacions.Add(model));
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
