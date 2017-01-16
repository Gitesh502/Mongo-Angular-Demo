using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Owin;
using KnowCostWeb.ChatUtilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BusinessService.Services;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace KnowCostWeb
{
   
    public class ChatAppHub : Hub
    {
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        private readonly IUserService _iuserservices;
        public ChatAppHub(IUserService IUserServices)
        {
            _iuserservices = IUserServices;
        }
        public void Connect(string email)
        {

            var User = _iuserservices.GetUserByEmail(email);
            //var id = Context.ConnectionId;
            //ConnectedUsers cu = new ConnectedUsers();
            //cu.UserID = User.Id;
            //cu.ConnectionId = id;
            //cu.UserName = userName;
            //cu.ConnectedTime = DateTime.Now;
            //cu.Name = userName;
            //_chatrepository.SaveConnectedUsers(cu);

            var id = Context.ConnectionId;
            var currentconnectionId=Context.ConnectionId;
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = email, Email = email,FirstName=User.FirstName,LastName=User.LastName,FullName=User.FullName });
                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string sJSON = oSerializer.Serialize(ConnectedUsers.Distinct());
                Clients.Caller.onConnected(id, email, sJSON, CurrentMessage);
                Clients.AllExcept(currentconnectionId).onNewUserConnected(id, email, email, User.FirstName, User.LastName, User.FullName);
            }
        }
        public void SendMessageToAll(string userName, string message,string Email)
        {
            AddMessageinCache(userName, message,Email);
            Clients.All.messageReceived(userName, message,Email);
        }
        public void SendPrivateMessage(string toUserId, string message)
        {

            string fromUserId = Context.ConnectionId;

            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {
                // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }

            return base.OnDisconnected(stopCalled);
        }
        #region Save cache

        private void AddMessageinCache(string userName, string message, string Email)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message,Email=Email });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        #endregion
    }
}