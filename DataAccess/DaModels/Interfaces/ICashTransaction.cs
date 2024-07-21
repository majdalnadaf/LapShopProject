using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface ICashTransaction
    {
        Task<bool> Delete(int id);


        Task<List<TbCashTransacion>> GetAll();


        Task<TbCashTransacion> GetById(int? id);


        Task<bool> Save(TbCashTransacion model);
    }
}
