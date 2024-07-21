using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Bl
{
    public class ClsRecommendedItem
    {
        public static async Task<List<TbItem>> GetRecommendedItems(IItem clsItem) 
        {
            try
            {

                var lstItems = (await clsItem.GetAll()).OrderBy(a => a.Rating).ToList();
                return lstItems;
            }
            catch 
            {
                return new List<TbItem>();
            }



        }
    }
}
