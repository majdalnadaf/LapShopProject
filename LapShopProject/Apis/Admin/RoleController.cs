using DataAccess.Identity;
using LapShopProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShopProject.Apis.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        DbLapShopContext context;
        public RoleController(RoleManager<ApplicationRole> roleManager, DbLapShopContext ctx)
        {
            _roleManager = roleManager;
            context = ctx;
        }


        // GET api/<RoleController>/5
        [HttpGet("{id}/{forAdmin}")]
        public string GetRoleActionById(string id, bool forAdmin)
        {
            try
            {
                List<ApplicationRole> lstRoles = _roleManager.Roles.ToList();
                var oRole = lstRoles.Where(a => a.Id == id).FirstOrDefault();
                if (oRole == null)
                {
                    return "";
                }

                if (forAdmin)
                    return oRole.WebsiteRoleActions;

                else
                    return oRole.AdminRoleActions;

            }
            catch (Exception)
            {

                throw new Exception();
            }
        }


        [HttpPost]
        public IActionResult UpdateRoleAction([FromBody] VwRole role)
        {
            try
            {
                List<ApplicationRole> lstRoles = _roleManager.Roles.ToList();
                var oRole = lstRoles.Where(a => a.Id == role.RoleId).FirstOrDefault();
                if (oRole == null)
                    return BadRequest(new { message = "Cant found the role " });

                if (role.IsAdmin)
                {

                    oRole.WebsiteRoleActions = role.RoleAction;

                }
                else
                {
                    oRole.AdminRoleActions = role.RoleAction;
                }

                context.Entry(oRole).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();

                return Ok(new { message = "RoleAction is updated" });

            }
            catch (Exception)
            {

                throw new Exception();
            }
        }


    }
}
