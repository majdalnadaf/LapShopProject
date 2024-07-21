using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsSlider : ISlider
    {
        public ClsSlider(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public bool Delete(int id)
        {
            try
            {
                var model = context.TbSliders.Where(a => a.SliderId == id).FirstOrDefault();
                if (model != null)
                {
                    context.TbSliders.Remove(model);
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TbSlider>> GetAll()
        {
            try
            {
                var lstSliders = await Task.FromResult(context.TbSliders.ToList());
                return lstSliders;
            }
            catch
            {
                return new List<TbSlider>();

            }

        }

        public async Task<TbSlider> GetById(int id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbSlider();

                var slider = await Task.FromResult(context.TbSliders.Where(a => a.SliderId == id ).FirstOrDefault());
                if (slider != null)
                    return slider;

                return new TbSlider();
            }
            catch
            {
                return new TbSlider();
            }
        }

        public async Task<bool> Save(TbSlider model)
        {
            try
            {
                if (model.SliderId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbSliders.Add(model));
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
