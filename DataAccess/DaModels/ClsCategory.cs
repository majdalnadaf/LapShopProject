using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsCategory : ICategory
    {
        public ClsCategory(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public async Task<bool> Delete(int  id)
        {
            try
            {
                var model =  context.TbCategories.Where(a => a.CategoryId == id).FirstOrDefault();
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

        public async Task<List<TbCategory>> GetAll()
        {
            try
            {
                var lstcategorys = await Task.FromResult(context.TbCategories.Where(a => a.CurrentState == 1).ToList()); 
                    return lstcategorys;
            }
            catch
            {
                return new List<TbCategory>();

            }
         
        }

        public async Task<TbCategory> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbCategory();

                var category = await Task.FromResult(context.TbCategories.Where(a => (a.CategoryId == id && a.CurrentState == 1)).FirstOrDefault());
                if (category != null)
                    return category;

                return new TbCategory();
            }
            catch
            {
                return new TbCategory();
            }
        }

        public async Task<bool> Save(TbCategory model)
        {
            try
            {
                if (model.CategoryId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbCategories.Add(model));
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
