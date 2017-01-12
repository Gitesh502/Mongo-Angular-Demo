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
//using System.Web.Http;
using KnowCostData.Entity;
using KnowCostData.Repository;


namespace KnowCostWeb
{
   
    public class ChatAppHub : Hub
    {
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        private readonly IChatRepository _chatrepository;
        private readonly IUserRepository _userrepository;
        public ChatAppHub(IChatRepository chatrepository,IUserRepository userRepository) 
        {
            _chatrepository = chatrepository;
            _userrepository = userRepository;
        }
        public void Connect(string userName)
        {

            var User = _userrepository.GetUserByEmail(userName);

            var id = Context.ConnectionId;
            ConnectedUsers cu = new ConnectedUsers();
            cu.UserID = User.Id;
            cu.ConnectionId = id;
            cu.UserName = userName;
            cu.ConnectedTime = DateTime.Now;
            cu.Name = userName;
            _chatrepository.SaveConnectedUsers(cu);
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