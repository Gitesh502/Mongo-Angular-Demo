using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BusinessService.Services;

namespace KnowCostWeb.Areas.Chat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Chat/Home
        private readonly IUserMessageService _iUserMessageService;
        public HomeController()
        {
            _iUserMessageService = new UserMessageService();
        }
        public ActionResult Index()
        {
           
            return View();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult MyProfile()
        {
            return View();
        }
    }
}