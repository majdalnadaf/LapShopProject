using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IDelivery
    {

        Task<bool> Delete(int id);


        Task<List<TbDelivery>> GetAll();


        Task<TbDelivery> GetById(int? id);


        Task<bool> Save(TbDelivery model);

    }
}
