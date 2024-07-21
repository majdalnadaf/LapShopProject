using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domains.Models;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IController
    {
        List<TbController> GetAll();
    }
}
