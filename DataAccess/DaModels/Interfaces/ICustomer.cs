using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface ICustomer
    {

        bool Save(TbCustomer model);
        bool Save(TbCustomer model , out int customerId);


    }
}
