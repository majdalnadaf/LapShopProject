using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsCountry : ICountry
    {

        public ClsCountry(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public  bool Delete(int id)
        {
            try
            {
                var model = context.TbCountries.Where(a => a.CountryId == id).FirstOrDefault();
                if (model != null)
                {
                    context.TbCountries.Remove(model);

                    context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TbCountry>> GetAll()
        {
            try
            {
                var lstCountries = await Task.FromResult(context.TbCountries.ToList());
                return lstCountries;
            }
            catch
            {
                return new List<TbCountry>();

            }

        }

        public async Task<TbCountry> GetById(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return new TbCountry();

                var country = await Task.FromResult(context.TbCountries.Where(a => (a.CountryId == id )).FirstOrDefault());
                if (country != null)
                    return country;

                return new TbCountry();
            }
            catch
            {
                return new TbCountry();
            }
        }

        public async Task<bool> Save(TbCountry model)
        {
            try
            {
                if (model.CountryId == 0)
                {
                    // function for implemmentation feilds
                    await Task.Run(() => context.TbCountries.Add(model));
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
