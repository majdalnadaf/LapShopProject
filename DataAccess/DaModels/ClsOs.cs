using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.DaModels
{


    public class ClsOs : IOs
    {

        public ClsOs(DbLapShopContext ctx)
        {
            context=ctx;
        }

        private readonly DbLapShopContext context;
        public async Task<bool> Delete(int id)
        {
            try
            {
                var model = context.TbOs.Where(a => a.OsId == id).FirstOrDefault();
                if (model != null)
                {
                    model.CurrentState = 0;
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

        public async Task<List<TbOs>> GetAll()
        {
            try
            {
                var lstItems = await Task.FromResult(context.TbOs.Where(a => a.CurrentState == 1).ToList());
                if (lstItems != null)
                    return lstItems;
            }
            catch
            {
                return new List<TbOs>();

            }
            return new List<TbOs>();
        }

        public async Task<TbOs> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbOs();

                var os = await Task.FromResult(context.TbOs.Where(a => (a.OsId == id && a.CurrentState == 1)).FirstOrDefault());
            
                if (os != null)
                    return os;

                return new TbOs();
            }
            catch
            {
                return new TbOs();
            }
        }

        public async Task<bool> Save(TbOs model)
        {
            try
            {
                if (model.OsId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbOs.Add(model));
                }
                else
                {
                    await Task.Run(() => context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified);
                }

                context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
               
            }

        }
    }
}
