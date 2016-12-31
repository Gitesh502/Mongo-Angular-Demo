using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowCostWeb.ApiModels
{
    public class UserDetailsModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public bool SendEmail { get; set; }
    }

    public class RegisterResponse
    {
        public int  SignInStatus { get; set; }
        public string ErrorString { get; set; }
    }
    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string returnUrl { get; set; }
    }
}