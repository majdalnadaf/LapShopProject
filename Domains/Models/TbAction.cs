using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    public class TbAction
    {
        public int Id { get; set; }
        public string ActionName { get; set; } = string.Empty;  
        public int ControllerId { get; set; }

        public virtual TbController Controller { get; set; } = null;

        
    }
}
