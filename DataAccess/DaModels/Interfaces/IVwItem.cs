
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models.VwAdmin;


namespace DataAccess.DaModels.Interfaces
{
    public interface IVwItem
    {
        List<VwItem> GetAll();
        VwItem GetById(int id); 
    }
}
