using KnowCostWeb.ApiModels;
using KnowCostWeb.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using MongoDB.Bson;

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
        private SignInHelper _helper;

        private SignInHelper SignInHelper
        {
            get
            {
                if (_helper == null)
                {
                    _helper = new SignInHelper(UserManager, AuthenticationManager);
                }
                return _helper;
            }
        }
        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<RegisterResponse> Register(UserDetailsModel model)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        FullName = model.FirstName + " " + model.LastName,
                        UserProfile = new UserProfile
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                        }

                    };
                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {

                        if (model.SendEmail)
                        {
                            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            var callbackUrl = Url.Link("Default", new { Controller = "Account", Action = "ConfirmEmail", userId = user.Id, code = code });
                            await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");

                        }
                        else
                        {
                            var Sresult = await SignInHelper.PasswordSignIn(model.Email, model.Password, false, shouldLockout: false);

                            switch (Sresult)
                            {
                                case SignInStatus.Success:
                                    response.SignInStatus = 1;
                                    // Add User Claims for full name. You can check for the success of addition 
                                    await UserManager.AddClaimAsync(user.Id, new Claim("FirstName", user.UserProfile.FirstName));
                                    await UserManager.AddClaimAsync(user.Id, new Claim("LastName", user.UserProfile.LastName));
                                    RedirectToRoute("Chat_default", new { Status = 1 });
                                    return response;
                                case SignInStatus.LockedOut:
                                    response.SignInStatus = 2;
                                    response.ErrorString = "Your account is lockedout please contact administrator";
                                    return response;

                                case SignInStatus.RequiresTwoFactorAuthentication:
                                    response.SignInStatus = 3;
                                    response.ErrorString = "Your account requires TwoFactorAuthentication please contact administrator";
                                    return response;
                                case SignInStatus.Failure:
                                    response.SignInStatus = 0;
                                    response.ErrorString = "Login failed!Please contact administrator";
                                    return response;
                                default:
                                    response.ErrorString = "Invalid login attempt.";
                                    return response;
                            }
                        }
                    }
                    else
                    {
                        string retErro = "";
                        foreach (string str in result.Errors)
                        {
                            retErro += str + "\n";
                        }
                        response.ErrorString = retErro;
                        response.SignInStatus = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorString = ex.Message;
                response.SignInStatus = -1;
            }
            return response;
        }

        [HttpPost]
        [Route("api/Account/Login")]
        public async Task<RegisterResponse> Login(UserLoginModel model)
        {
            User.Identity.GetUserId();
            RegisterResponse response = new RegisterResponse();
            if (!ModelState.IsValid)
            {
                return response;
            }

            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            try
            {
                var result = await SignInHelper.PasswordSignIn(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        response.SignInStatus = 1;
                        var UserDetails = await UserManager.FindByEmailAsync(model.Email);
                        UserDetailsModel loggedUser = new UserDetailsModel();
                        loggedUser.FirstName = UserDetails.UserProfile.FirstName;
                        loggedUser.LastName = UserDetails.UserProfile.LastName;
                        loggedUser.strUserID = UserDetails.Id;
                        loggedUser.Email = UserDetails.UserName;
                        response.LoggedUserDetails = loggedUser;
                        RedirectToRoute("Chat_default", new { Status = 1 });
                        return response;
                    case SignInStatus.LockedOut:
                        response.SignInStatus = 2;
                        response.ErrorString = "Your account is lockedout please contact administrator";
                        return response;

                    case SignInStatus.RequiresTwoFactorAuthentication:
                        response.SignInStatus = 3;
                        response.ErrorString = "Your account requires TwoFactorAuthentication please contact administrator";
                        return response;
                    case SignInStatus.Failure:
                        response.SignInStatus = 0;
                        response.ErrorString = "Login failed!Please contact administrator";
                        return response;
                    default:
                        response.ErrorString = "Invalid login attempt.";
                        return response;
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }



        internal class ChallengeResult : System.Web.Mvc.HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(System.Web.Mvc.ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}