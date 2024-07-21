using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IItemDiscount
    {
        List<TbItemDiscount> GetAll(int itemId);
        List<TbItemDiscount> GetAll();

        TbItemDiscount GetById(int discountId);
        bool Delete(int discountId);

        bool Save(TbItemDiscount model);



    }
}
