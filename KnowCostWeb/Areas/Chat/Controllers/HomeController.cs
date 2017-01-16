using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace KnowCostWeb.Areas.Chat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Chat/Home

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