using BusinessService.Services;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KnowCostWeb.Api
{
    public class UserMessageController : ApiController
    {
        private readonly IConversationService _iconversationservice;
        public UserMessageController(IConversationService IConversationService)
        {
            this._iconversationservice = IConversationService;
        }
        [HttpGet]
        [Route("api/UserMessage/GetUserMessages")]
        public HttpResponseMessage GetRegisteredUsers()
        {

            //var userdetail = _userMessageService.GetMessages();
            //if (userdetail != null)
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, userdetail);
            //}
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No data found");
        }
        [HttpGet]
        [Route("api/UserMessage/GetMessagesByUserId")]
        public HttpResponseMessage GetMessagesByUserId(string toUserId)
        {
            toUserId = User.Identity.GetUserId();
            var userdetail = _iconversationservice.GetPreviousMessagesByUserId(toUserId);
            if (userdetail != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, userdetail);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No data found");
        }
        [HttpGet]
        [Route("api/UserMessage/GetMessagesByConversationId")]
        public HttpResponseMessage GetMessagesByConversationId(string fromUserId, string toUserId)
        {
            //toUserId = User.Identity.GetUserId();
            var userdetail = _iconversationservice.GetPreviousMessagesByConversationId(fromUserId, toUserId, 0);
            if (userdetail != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, userdetail);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No data found");
        }
    }
}