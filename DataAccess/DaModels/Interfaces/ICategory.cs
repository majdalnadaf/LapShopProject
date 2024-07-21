using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface ICategory
    {

         Task<bool> Delete(int id);


         Task<List<TbCategory>> GetAll();


         Task<TbCategory> GetById(int? id);


         Task<bool> Save(TbCategory model);

    }
}

