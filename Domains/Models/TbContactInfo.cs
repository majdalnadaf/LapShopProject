using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    public class TbContactInfo
    {
        public int Id { get; set; }
        
        public string PhoneNumber1 { get; set; } = "+963 0937675713";
        public string PhoneNumber2 { get; set;} = "+963 0937675713";

        public string Address { get; set; } = "Syria - Hama - Salamieh";  

        public string SupportEmail1 { get; set; } = "majdalnadaf8@gmail.com";
        public string SupportEmail2 { get; set; } = "majdalnadaf8@gmail.com";

        public string ChatEmail { get; set; } = "majdalnadaf06@gmail.com";

        public string ChatPhone { get; set; } = "+963 0937675713";



    }
}
