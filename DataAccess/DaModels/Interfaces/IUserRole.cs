using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace DataAccess.DaModels.Interfaces
{
    public interface IUserRole
    {

        List<IdentityUserRole<string>> GetAll();
        IdentityUserRole<string> GetById(string id , bool byUserId);

        bool Delete(string id, bool byUserId);
        bool Update(IdentityUserRole<string> model);


    }
}
