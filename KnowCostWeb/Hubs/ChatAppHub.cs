using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Owin;
using KnowCostWeb.ChatUtilities;
using Microsoft.AspNet.Identity;
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
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName,Email=userName });
                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string sJSON = oSerializer.Serialize(ConnectedUsers.Distinct());
                Clients.Caller.onConnected(id, userName, sJSON, CurrentMessage);
                string email = userName;
                Clients.AllExcept(id).onNewUserConnected(id, userName, email);
            }
        }
        public void SendMessageToAll(string userName, string message,string Email)
        {
            AddMessageinCache(userName, message,Email);
            Clients.All.messageReceived(userName, message,Email);
        }

        #region private Messages

        private void AddMessageinCache(string userName, string message, string Email)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message,Email=Email });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        #endregion
    }
}