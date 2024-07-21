using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DaModels.Interfaces;
using Domains.Models;

namespace DataAccess.DaModels
{
    public class ClsSetting : ISetting
    {


        public ClsSetting(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public bool Delete(int id)
        {
            try
            {
                var model = context.TbSettings.Where(a => a.SettingId == id).FirstOrDefault();
                if (model == null)
                   return false;

                context.TbSettings.Remove(model);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TbSettings> GetAll()
        {
            try 
            {
                var lstSettings = context.TbSettings.ToList();
                if (lstSettings == null)
                    return new List<TbSettings>();

                return lstSettings;
            }
            catch
            { 
            
                return new List<TbSettings>();  
            }
        }

        public TbSettings GetById(int id)
        {
            try
            {
                var Settings = context.TbSettings.Where(a => a.SettingId == id).FirstOrDefault();
                if (Settings == null)
                    return new TbSettings();

                return Settings;
            }
            catch
            {

                return new TbSettings();

            }
        }

        public async Task<bool> Save(TbSettings model)
        {
            try
            {
                if (model.SettingId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbSettings.Add(model));
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

        public TbSettings GetFirst()
        {
            try
            {
                var Settings = context.TbSettings.FirstOrDefault();
                if (Settings == null)
                    return new TbSettings();

                return Settings;
            }
            catch
            {

                return new TbSettings();

            }
        }
    }
}
