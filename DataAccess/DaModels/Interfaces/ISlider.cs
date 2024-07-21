using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface ISlider
    {
        Task<List<TbSlider>> GetAll();

        Task<TbSlider> GetById(int id);

        bool Delete(int id);

       Task<bool> Save(TbSlider slider);
 
    }
}
