using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Domains.Models.VwAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsItemImage : IItemImage
    {
        public ClsItemImage(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;

        public List<TbItemImage> GetAll(int itemId)
        {
            try
            {
                var lstItemImages = context.TbItemImages.Where(a => a.ItemId == itemId).ToList();
                return lstItemImages;   

            }
            catch 
            {
                return new List<TbItemImage>();
            }
        }

        public List<TbItemImage> GetAll()
        {
            try
            {
                var lstItemImages = context.TbItemImages.ToList();
                return lstItemImages;

            }
            catch
            {
                return new List<TbItemImage>();
            }
        }

        public TbItemImage GetById(int imageId)
        {
            try
            {
                var image = context.TbItemImages.Where(a => a.ImageId == imageId).FirstOrDefault();
                if(image==null)
                    return new TbItemImage();

                return image;
            }
            catch 
            {
                return new TbItemImage();
            }

        }

        public bool Delete(int Imageid)
        {
            try
            {
                var image = GetById(Imageid);   
                context.TbItemImages.Remove(image);
                context.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool Save(TbItemImage model)
        {
            try
            {
                if (model.ImageId == 0)
                {
                    // implement properties
                    context.TbItemImages.Add(model);

                }
                else 
                {
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

