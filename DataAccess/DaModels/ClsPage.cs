using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsPage : IPage
    {

        private readonly DbLapShopContext context;

        public ClsPage(DbLapShopContext ctx)
        {
            context = ctx;        
        }

        public bool Delete(int pageId)
        {
            try
            {
                if(pageId == 0 )
                    return false;

                var oPage = GetById(pageId);
                context.TbPages.Remove(oPage);

                context.SaveChanges();
                
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<TbPage> GetAll()
        {
            try
            {
                var lstPages = context.TbPages.ToList();
                if (lstPages == null)
                    return new List<TbPage>();

                return lstPages;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TbPage GetById(int pageId)
        {

            try
            {
                var oPage = context.TbPages.Where(a=> a.PageId == pageId).FirstOrDefault();
                if (oPage == null)
                    return new TbPage();

                return oPage;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool Save(TbPage model)
        {
            try
            {
                if (model == null)
                    return false;


                if(model.PageId == 0)
                {
                    context.TbPages.Add(model);

                }
                else 
                {
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                context.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message); 
            }
        }
    }
}
