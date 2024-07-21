using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsItemType : IItemType
    {

        public ClsItemType(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public async Task<bool> Delete(int id)
        {
            try
            {
                var model = context.TbItemTypes.Where(a => a.ItemTypeId == id).FirstOrDefault();
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

        public async Task<List<TbItemType>> GetAll()
        {
            try
            {
                var lstItems = await Task.FromResult(context.TbItemTypes.Where(a => a.CurrentState == 1).ToList());
                if (lstItems != null)
                    return lstItems;
            }
            catch
            {
                return new List<TbItemType>();

            }
            return new List<TbItemType>();
        }

        public async Task<TbItemType> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbItemType();

                var itemType = await Task.FromResult(context.TbItemTypes.Where(a => (a.ItemTypeId == id && a.CurrentState == 1)).FirstOrDefault());
                if (itemType != null)
                    return itemType;

                return new TbItemType();
            }
            catch
            {
                return new TbItemType();
            }
        }

        public async Task<bool> Save(TbItemType model)
        {
            try
            {
                if (model.ItemTypeId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbItemTypes.Add(model));
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
