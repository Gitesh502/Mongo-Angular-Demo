using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowCostWeb.ChatUtilities
{
   
    public class UserDetail
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }

    public class MessageDetail
    {

        public string UserName { get; set; }

        public string Message { get; set; }
        public string Email { get; set; }

    }

    public class PrivateChatMessage
    {
        public string userName { get; set; }
        public string message { get; set; }
    }
}