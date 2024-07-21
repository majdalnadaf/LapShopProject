using Domains.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IItem
    {

        Task<List<TbItem>> GetAll();
        Task<TbItem> GetById(int? id);

        Task<bool> Save(TbItem model);

         Task<bool> Delete(int id);

    }
}
