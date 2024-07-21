using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsCustomer : ICustomer
    {
        private readonly DbLapShopContext context;
        public ClsCustomer(DbLapShopContext ctx)
        {
                context = ctx;
        }

        public bool Save(TbCustomer model , out int customerId)
        {
			try
			{

                if (model == null)
                {
                    customerId = 0;
                    return false;
                }


                if (model.CustomerId == 0)
                {

                    context.TbCustomers.Add(model);


                }
                else 
                {
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

               
                context.SaveChanges();

                customerId = model.CustomerId;
                return true;

			}
 			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
        }

        public bool Save(TbCustomer model)
        {
            try
            {

                if (model == null)
                    return false;

                if (model.CustomerId == 0)
                {

                    context.TbCustomers.Add(model);


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
