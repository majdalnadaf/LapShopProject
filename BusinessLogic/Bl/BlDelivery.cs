using DataAccess;
using DataAccess.DaModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace BusinessLogic.Bl
{
    public class BlDelivery
    {

        IDelivery clsDelivery;

        public BlDelivery(   IDelivery oDelivery)
        {

            clsDelivery = oDelivery;    
        }

        /// <summary>
        /// Get the delivery man by min Count of Delivery Event 
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetDeliveryId() 
        {
          var lstDelivery = await clsDelivery.GetAll();

            int nMinIdDelivey = 0;
            int nMinCountOfDelivery = int.MaxValue;
            foreach (var delivery in lstDelivery) 
            {
                if (delivery.CountOfDeliveryEvent < nMinCountOfDelivery)
                {
                    nMinCountOfDelivery = delivery.CountOfDeliveryEvent;
                    nMinIdDelivey = delivery.DeliveryId;
                }

            }

            return nMinIdDelivey;
        }
    }
}
