using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Owin;
using KnowCostWeb.ChatUtilities;

namespace KnowCostWeb
{
    public class ChatAppHub : Hub
    {
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        public void Connect(string userName)
        {

            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {

                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName });
                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string sJSON = oSerializer.Serialize(ConnectedUsers);
                // send to caller
                Clients.Caller.onConnected(id, userName, sJSON, CurrentMessage);

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName);

            }

        }


    }
}