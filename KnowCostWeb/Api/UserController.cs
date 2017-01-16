
//using KnowCostData.Entity;
//using KnowCostData.Repository;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessService.Services;

namespace KnowCostWeb.Api
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService UserService)
        {
            this._userService = UserService;
        }
        [HttpGet]
        [Route("api/User/GetUserByEmail")]
        public HttpResponseMessage GetUserByEmail(string email)
        {
            var userdetail = _userService.GetUserByEmail(email);
            if (userdetail != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, userdetail);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No User found with email" + email);
        }
        [HttpGet]
        [Route("api/User/GetRegisteredUsers")]
        public HttpResponseMessage GetRegisteredUsers()
        {

            var userdetail = _userService.GetRegisteredUsers();
            if (userdetail != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, userdetail);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No data found");
        }
    }
}