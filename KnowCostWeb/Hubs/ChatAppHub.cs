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
using BusinessEntities.BusinessEntityModels;

namespace KnowCostWeb
{
   
    public class ChatAppHub : Hub
    {
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<ChatUtilities.ConnectedUsers> OnlineUsers = new List<ChatUtilities.ConnectedUsers>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        private readonly IUserService _iuserservices;
        private readonly IConnectionMappingService _iConnectionMappingService;
        public ChatAppHub(IUserService IUserServices,IConnectionMappingService IConnectionMappingService)
        {
            _iuserservices = IUserServices;
        }
        public void Connect(string NickName)
        {
            string email = Context.User.Identity.GetUserName();
            var User = _iuserservices.GetUserByEmail(email);
            var id = Context.ConnectionId;
            var currentconnectionId=Context.ConnectionId;
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ChatUtilities.ConnectedUsers objcu = new ChatUtilities.ConnectedUsers();
                objcu.ConnectionId = id;
                objcu.Email = User.Email;
                objcu.UserName = User.UserName;
                objcu.UserId = User.Id.ToString();
                objcu.FirstName = User.FirstName;
                objcu.LastName = User.LastName;
                objcu.FullName = User.FullName;
                objcu.NickName = NickName;

                OnlineUsers.Add(objcu);

                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                string sJSON = oSerializer.Serialize(OnlineUsers.Distinct());

                Clients.Caller.onConnected(id,sJSON, CurrentMessage);

                Clients.AllExcept(id).onNewUserConnected(objcu);
            }
        }
        public override Task OnConnected()
        {
            var id = Context.ConnectionId;
            var userId = Context.User.Identity.GetUserId();
            ConnectionMappingsEntity objCom = new ConnectionMappingsEntity();
            objCom.ConnectionId = id;
            objCom.UserId = userId;
            objCom.IsActive = true;
            objCom.ConnectedOn = DateTime.Now;
            _iConnectionMappingService.SaveConnectionMapping(objCom);
            return base.OnConnected();
        }
        public void SendMessageToAll(string userName, string message,string Email)
        {
            AddMessageinCache(userName, message,Email);
            Clients.All.messageReceived(userName, message,Email);
        }
        public void SendPrivateMessage(string toUserId, string message)
        {

            string fromUserId = Context.ConnectionId;

            var toUser = OnlineUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = OnlineUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

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
            var item = OnlineUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                OnlineUsers.Remove(item);

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