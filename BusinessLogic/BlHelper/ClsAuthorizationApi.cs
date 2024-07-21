using DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BlHelper
{
    public static class ClsAuthorizationApi
    {

        public static ApplicationUser AuthorizeUser(ApplicationUser user)
        {

            if (user.FirstName == "Majd" && user.LastName == "Nadaf")
                return user;
            // check if the user exists in database or json file in server
            
            //if yes return it 

            return null;
        }

    }
}
