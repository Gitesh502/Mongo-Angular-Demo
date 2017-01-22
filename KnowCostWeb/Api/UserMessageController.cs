using BusinessService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}