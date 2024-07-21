using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IItemImage
    {
        List<TbItemImage> GetAll(int itemId);
        List<TbItemImage> GetAll();
        TbItemImage GetById(int imageId);

        bool Delete(int id);

        bool Save(TbItemImage model);
    }
}
