using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IOs
    {
        Task<List<TbOs>> GetAll();
        Task<TbOs> GetById(int? id);

        Task<bool> Save(TbOs model);

         Task<bool> Delete(int id);
    }
}
