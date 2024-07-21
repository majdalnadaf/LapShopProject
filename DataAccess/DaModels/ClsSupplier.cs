using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DaModels.Interfaces;
using Domains.Models;

namespace DataAccess.DaModels
{
    public class ClsSupplier : ISupplier
    {
        public ClsSupplier(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public bool Delete(int id)
        {
            try
            {
                var model = context.TbSuppliers.Where(a => a.SupplierId == id).FirstOrDefault();
                if (model != null)
                {

                    context.TbSuppliers.Remove(model);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TbSupplier>> GetAll()
        {
            try
            {
                var lstSuppliers = await Task.FromResult(context.TbSuppliers.ToList());
                return lstSuppliers;
            }
            catch
            {
                return new List<TbSupplier>();

            }

        }

        public async Task<TbSupplier> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbSupplier();

                var supplier = await Task.FromResult(context.TbSuppliers.Where(a => (a.SupplierId == id )).FirstOrDefault());
                if (supplier != null)
                    return supplier;

                return new TbSupplier();
            }
            catch
            {
                return new TbSupplier();
            }
        }

        public async Task<bool> Save(TbSupplier model)
        {
            try
            {
                if (model.SupplierId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbSuppliers.Add(model));
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
