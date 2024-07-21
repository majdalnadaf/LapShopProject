using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsReview : IReview
    {
        private readonly DbLapShopContext context;
        public ClsReview(DbLapShopContext ctx) 
        {
            context = ctx;  
        }  
        public bool Save(TbReview review)
        {
            
                try
                {
                // implement model properties
                  context.TbReviews.Add(review);
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
