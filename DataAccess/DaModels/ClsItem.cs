
using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsItem : IItem
    {

        public ClsItem(DbLapShopContext ctx) 
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public  async Task< bool> Delete(int id)
        {
            try
            {
                var model = context.TbItems.Where(a => a.ItemId == id).FirstOrDefault();

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

        public async Task<List<TbItem>> GetAll()
        {
            try
            {
                var lstItems = await Task.FromResult(context.TbItems.Where(a=> a.CurrentState==1).ToList());
                if (lstItems != null)
                    return lstItems;
            }
            catch
            {
                return new List<TbItem>();

            }
            return new List<TbItem>();
        }

        public async Task<TbItem> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbItem();

               var item =  await Task.FromResult(context.TbItems.Where(a => (a.ItemId == id && a.CurrentState==1)).FirstOrDefault());
                if (item != null)
                    return item;

                return new TbItem();
            }
            catch 
            {
                return new TbItem();
            }
        }

        public async Task<bool> Save(TbItem model)
        {
            try 
            {
                if (model.ItemId == 0)
                {
                    // function for implemmentation feilds
                   
                    // function for implemmentation feilds

                    await Task.Run(() => context.TbItems.Add(model));
                }
                else 
                {
                     await Task.Run(()=> context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified);
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
