using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;
namespace DataAccess.DaModels.Interfaces
{
    public interface ICountry
    {
        Task<List<TbCountry>> GetAll();
        Task<TbCountry> GetById(int? id);

        Task<bool> Save(TbCountry country);

       bool Delete(int id);    
    }
}
