using BusinessService.Services;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KnowCostWeb.Api
{
    public class UserMessageController : ApiController
    {
         private readonly IUserMessageService _userMessageService;
        public UserMessageController(IUserMessageService UserMessageService)
        {
            this._userMessageService = UserMessageService;
        }
        [HttpGet]
        [Route("api/UserMessage/GetUserMessages")]
        public HttpResponseMessage GetRegisteredUsers()
        {

            var userdetail = _userMessageService.GetMessages();
            if (userdetail != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, userdetail);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No data found");
        }
        [HttpGet]
        [Route("api/UserMessage/GetMessagesByUserId")]
        public HttpResponseMessage GetMessagesByUserId(string toUserId)
      {
            toUserId = User.Identity.GetUserId();
            var userdetail = _userMessageService.GetMessagesByUserId(toUserId);
            if (userdetail != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, userdetail);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No data found");
        }
    }
}