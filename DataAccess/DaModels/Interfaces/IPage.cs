using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IPage
    {
        List<TbPage> GetAll();
        TbPage GetById(int pageId);

        bool Save(TbPage model);

        bool Delete(int pageId);
    }
}
