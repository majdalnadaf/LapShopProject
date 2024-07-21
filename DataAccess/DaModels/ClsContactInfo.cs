using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsContactInfo : IContactInfo
    {
        public ClsContactInfo(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public bool Delete(int id)
        {
            try
            {
                var model = context.TbContactInfos.Where(a => a.Id == id).FirstOrDefault();
                if (model != null)
                {
                    context.Remove(model);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TbContactInfo>> GetAll()
        {
            try
            {
                var lstContactInfos = await Task.FromResult(context.TbContactInfos.ToList());
                return lstContactInfos;
            }
            catch
            {
                return new List<TbContactInfo>();

            }

        }

        public async Task<TbContactInfo> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbContactInfo();

                var contactInfo = await Task.FromResult(context.TbContactInfos.Where(a => a.Id == id).FirstOrDefault());
                if (contactInfo != null)
                    return contactInfo;

                return new TbContactInfo();
            }
            catch
            {
                return new TbContactInfo();
            }
        }

        public async Task<bool> Save(TbContactInfo model)
        {
            try
            {
                if (model.Id == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbContactInfos.Add(model));
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
