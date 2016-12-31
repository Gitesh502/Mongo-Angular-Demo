using AspNet.Identity.MongoDB;
using KnowCostWeb.ApiModels;
using KnowCostWeb.Models;
using KnowCostWeb.MongoUtilities;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Web;
using System.Web.Http;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace KnowCostWeb.Api
{
    
    public class AccountController : ApiController
    {

        private ApplicationUserManager _userManager;
        public AccountController()
        {

        }
        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            //SignInManager = signInManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<dynamic> Register(UserDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    UserName = model.Email,
                    Email = model.Email,
                    UserProfile = new UserProfile
                    {
                        FirstName=model.FirstName,
                        LastName=model.LastName,
                        Email=model.Email,
                    }
                
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                   // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    //ViewBag.Link = callbackUrl;
                    //return View("DisplayEmail");
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return "";
        }
    }
}