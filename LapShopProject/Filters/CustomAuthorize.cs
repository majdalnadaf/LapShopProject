using DataAccess;
using DataAccess.Identity;
using LapShopProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;



namespace LapShopProject.Filters
{



    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Note : I didnt divide this fucntion to sub Funtions for easy understanding 

            try
            {


                #region Inject the user manager and the sigin manager and role manager gion

                var _userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var _signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();
                var _roleManager = context.HttpContext.RequestServices.GetRequiredService<RoleManager<ApplicationRole>>();
                #endregion



                #region Get the area and controller and action name
                // Access the action that user want to access it 
                var actionName = context.HttpContext.Request.RouteValues["action"];
                // Access the controller that user want to access it
                var controllerName = context.HttpContext.Request.RouteValues["controller"];

                // Access the area user want to access it , /Admin or /Website 
                var areaName = context.ActionDescriptor.RouteValues["area"];
                #endregion



                #region Access the current user that sign up
                var currentUser = await _signInManager.UserManager.GetUserAsync(context.HttpContext.User);
                if (currentUser == null)
                {
                    if (areaName == "Admin")
                        context.Result = new RedirectToActionResult("Login", "User", null);

                    else
                        context.Result = new RedirectToActionResult("Login", "UserAccount", null);

                    
                    
                    return;
                }
                #endregion



                #region Get all roles of user , mybe the user has more than one role
                var lstUsersRoles = await _userManager.GetRolesAsync(currentUser);
                #endregion


                #region Get all roles from database 
                List<ApplicationRole> Roles = _roleManager.Roles.ToList();
                #endregion




                #region Determin the denied page depend on the area
                var denidAction = "Denied";
                var deniedController = string.Empty;

                if (areaName == "Admin")
                {
                    deniedController = "User";
                }
                else
                {

                    deniedController = "UserAccount";
                }
                #endregion


                #region Get the json file and check authorization

                // Declear jsonRoleAction that i get it from database  as properties of role 
                string jsonRoleAction = string.Empty;
                ApplicationRole roleOfUser = null;

                // Declear boolean variable for result of the specifice controller and action authroization 
                bool isValid = false;


                // Loop for all roles of user
                for (int i = 0; i < lstUsersRoles.Count; i++)
                {
                    roleOfUser = null;

                    // Find the role by role name as ApplicationRole 
                    roleOfUser = Roles.Find(a => a.Name == lstUsersRoles[i]);


                    if (roleOfUser == null)
                        continue;

                    // Get the  currect json file for admin area or website area 
                    if (areaName == "Admin")
                        jsonRoleAction = roleOfUser.AdminRoleActions;
                    else
                        jsonRoleAction = roleOfUser.WebsiteRoleActions;

                    // DeSerialize the json file 

                    List<Dictionary<string, List<Dictionary<string, bool>>>> data = JsonConvert.DeserializeObject<List<Dictionary<string, List<Dictionary<string, bool>>>>>(jsonRoleAction);



                    if (data == null)
                        continue;



                    // Get the boolean status of specifice controller and action the user accesss them
                    // for performance this loop to count 11 steps
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data[j].Keys.Contains(controllerName.ToString()))
                        {
                            // for performace this loop to maximum 4 steps
                            for (int k = 0; k < data[j][controllerName.ToString()].Count; k++)
                            {
                                if (data[j][controllerName.ToString()][k].Keys.Contains(actionName.ToString()))
                                {
                                    isValid = data[j][controllerName.ToString()][k][actionName.ToString()];
                                    break;
                                }
                            }

                        }
                    } //Details


                    // check if the role of user is valid to access the controller and action 
                    if (isValid)
                    {
                        // allow to user access page 
                        await base.OnActionExecutionAsync(context, next);

                    }


                }





                // Go to the denied page 
                context.Result = new RedirectToActionResult(denidAction, deniedController, null);
                #endregion


            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
    }
}
