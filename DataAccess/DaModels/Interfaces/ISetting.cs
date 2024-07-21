using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface ISetting
    {

        List<TbSettings> GetAll();

        TbSettings GetFirst();

        TbSettings GetById(int id);

        bool Delete(int id);

        Task<bool> Save(TbSettings model);
    }
}
