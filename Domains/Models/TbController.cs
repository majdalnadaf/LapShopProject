using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    public class TbController
    {
        public int Id {get; set;}
        public string ControllerName { get; set; } = string.Empty;

        public virtual ICollection<TbAction> LstActions { get; set; } = new List<TbAction>();   

    }
}
