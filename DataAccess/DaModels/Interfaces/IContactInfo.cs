using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IContactInfo
    {
        bool Delete(int id);


        Task<List<TbContactInfo>> GetAll();


        Task<TbContactInfo> GetById(int? id);


        Task<bool> Save(TbContactInfo model);
    }
}
