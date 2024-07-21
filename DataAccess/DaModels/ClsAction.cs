using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsAction : IAction
    {
        DbLapShopContext context;
        public ClsAction(DbLapShopContext ctx)
        {
            context = ctx;
        }
        public List<TbAction> GetAll()
        {
            try
            {
                var lstActions = context.TbActions.ToList();
                if (lstActions == null)
                    return new List<TbAction>();

                return lstActions;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
