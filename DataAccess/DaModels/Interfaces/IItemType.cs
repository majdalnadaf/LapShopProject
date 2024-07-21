using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IItemType
    {

        Task<List<TbItemType>> GetAll();
        Task<TbItemType> GetById(int? id);

        Task<bool> Save(TbItemType model);

         Task<bool> Delete(int id);
    }
}
