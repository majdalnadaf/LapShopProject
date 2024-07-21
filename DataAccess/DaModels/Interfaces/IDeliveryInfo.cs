using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IDeliveryInfo
    {

        Task<bool> Delete(int id);


        Task<List<TbDeliveryInfo>> GetAll();


        Task<TbDeliveryInfo> GetById(int? id);


        Task<bool> Save(TbDeliveryInfo model);

        bool SaveWithoutAsync(TbDeliveryInfo model);

    }
}
