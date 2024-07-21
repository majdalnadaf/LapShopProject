using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsController : IController
    {
        DbLapShopContext context;
        public ClsController(DbLapShopContext ctx)
        {
                context = ctx;
        }
        public List<TbController> GetAll()
        {
            try
            {
                var lstControlers = context.TbControllers.ToList();
                if (lstControlers == null)
                    return new List<TbController>();

                return lstControlers;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
