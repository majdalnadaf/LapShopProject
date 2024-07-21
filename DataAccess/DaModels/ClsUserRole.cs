using DataAccess.DaModels.Interfaces;
using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsUserRole : IUserRole
    {

        DbLapShopContext context;
        public ClsUserRole(DbLapShopContext ctx)
        {
            context = ctx;
        }

        public bool Delete(string id , bool byUserId)
        {
            try
            {
                var oUserRole = GetById(id, byUserId);
                if (oUserRole == null)
                    return false;

                context.UserRoles.Remove(oUserRole);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw new Exception();
            }
        }

        public List<IdentityUserRole<string>> GetAll()
        {
            try
            {
                var lstUserRole = context.UserRoles.ToList();
                if (lstUserRole == null)
                    return new List<IdentityUserRole<string>>();

                return lstUserRole;

            }
            catch (Exception)
            {
                return new List<IdentityUserRole<string>>();
                throw new Exception();
            }
        }

        public IdentityUserRole<string> GetById(string id , bool byUserId)
        {
            try
            {
                var oUserRole = new IdentityUserRole<string>();
                if (byUserId)
                {
                     oUserRole = context.UserRoles.FirstOrDefault(x => x.UserId == id);
                }
                else 
                {
                    oUserRole = context.UserRoles.FirstOrDefault(x => x.RoleId==id);
                }

                if (oUserRole == null)
                   return new IdentityUserRole<string>();

                return oUserRole;
            }
            catch (Exception)
            {
                return new IdentityUserRole<string>();
                throw new Exception();
            }
        }

        public bool Update(IdentityUserRole<string> model)
        {
            try
            {

                var oUserRole = GetById(model.UserId, true);
                if (oUserRole != null)
                {
                    context.UserRoles.Remove(oUserRole);
                }

                context.UserRoles.Add(model);

                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
            
                throw new Exception();
            }
        }


        
    }
}
