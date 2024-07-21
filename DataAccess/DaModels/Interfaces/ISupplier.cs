using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface ISupplier
    {

        bool Delete(int id);


        Task<List<TbSupplier>> GetAll();


        Task<TbSupplier> GetById(int? id);


        Task<bool> Save(TbSupplier model);
    }
}
