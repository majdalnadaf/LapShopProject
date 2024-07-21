using DataAccess.DaModels.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models.VwAdmin;

namespace DataAccess.DaModels
{
    public class ClsVwItem : IVwItem
    {
        public ClsVwItem(DbLapShopContext ctx)
        {
            context = ctx;
        }

        private readonly DbLapShopContext context;
        public List<VwItem> GetAll()
        {
            var lstVwItem = context.VwItems.Where(a=> a.CurrentState==1).ToList();

            if(lstVwItem!=null)
            return lstVwItem;

            return new List<VwItem>();
        }


        public VwItem GetById(int id) 
        {
            if (id == 0) 
            {
                return new VwItem();
            }

            var vwItem = context.VwItems.Where(a => a.ItemId == id).FirstOrDefault();
            if(vwItem!=null)
                return vwItem;

            return new VwItem();
        }
    }
}
