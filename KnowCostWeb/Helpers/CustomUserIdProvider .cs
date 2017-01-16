using BusinessService.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowCostWeb.Helpers
{
    public class CustomUserIdProvider : Controller,IUserIdProvider
    {
        private readonly IUserService _iuserservices;
        public CustomUserIdProvider(IUserService IUserServices)
        {
            _iuserservices = IUserServices;
        }
        public CustomUserIdProvider()
        {
            _iuserservices = new UserService();
        }
        public string GetUserId(IRequest request)
        {
            // your logic to fetch a user identifier goes here.

            // for example:
            var email = request.User.Identity.Name;
            var User = _iuserservices.GetUserByEmail(email);
            string UserId = User.Id.ToString();
            return UserId;
        }
    }
}