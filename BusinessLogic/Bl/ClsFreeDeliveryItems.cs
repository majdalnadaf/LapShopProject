using DataAccess.DaModels.Interfaces;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Bl
{
    public class ClsFreeDeliveryItems
    {
        public static async Task<List<TbItem>> GetFreeDeliveryItems(IItem clsItem)
        {
            try
            {
                // for this business , free delivery items are items have low rating
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
