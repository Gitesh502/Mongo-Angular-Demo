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
using MongoDB.Bson;

namespace KnowCostWeb
{
   
    public class ChatAppHub : Hub
    {
        static List<string> ConnectedUserIds = new List<string>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        static List<ChatUtilities.ConnectedUsers> OnlineUsers = new List<ChatUtilities.ConnectedUsers>();

        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
      
      
        private readonly IUserService _iuserservices;
        private readonly IConnectionMappingService _iConnectionMappingService;
        private readonly IUserMessageService _iUserMessageService;
        public ChatAppHub(IUserService IUserServices,IConnectionMappingService IConnectionMappingService,IUserMessageService IUserMessageService)
        {
            _iuserservices = IUserServices;
            _iConnectionMappingService = IConnectionMappingService;
            _iUserMessageService = IUserMessageService;
        }
        public void Connect(string NickName)
        {
            var id = Context.User.Identity.GetUserId();
            ConnectedUserIds.Add(id);
            ConnectedUserIds = ConnectedUserIds.Distinct().ToList();
           


            string email = Context.User.Identity.GetUserName();

            var User = _iuserservices.GetUserByEmail(email);

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
                objcu.IsPriavteBoxOpened = false;
                
                OnlineUsers.Add(objcu);


                #region Make clien calls for onconnected for current user and to get notification for other users

                //Serialize a list which is need to send to client side
                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string sJSONOnlineUsers = oSerializer.Serialize(OnlineUsers.Distinct());

                Clients.User(id).onConnected(id, sJSONOnlineUsers, CurrentMessage);

                string responseObj = oSerializer.Serialize(objcu);
                List<string> OtherConnectedUsers = ConnectedUserIds.Where(a => a != id).ToList();
                Clients.Users(OtherConnectedUsers).onNewUserConnected(id, responseObj, CurrentMessage);

                #endregion
            }
        }
        public override Task OnConnected()
        {
            var id = Context.User.Identity.GetUserId();
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
            string fromUserId = Context.User.Identity.GetUserId();
            var fromFullName = OnlineUsers.FirstOrDefault(x => x.ConnectionId == fromUserId).FullName;
            AddMessageinCache(userName, message,Email);
            Clients.Users(ConnectedUserIds).messageReceived(userName, message, Email, fromFullName);

            var exceptThisUser = ConnectedUserIds.Where(a => a != fromUserId).ToList();
            UserMessagesEntity ume = new UserMessagesEntity();
            ume.UserId = fromUserId;
            ume.MessageId = Guid.NewGuid().ToString();
            ume.MessageDescription = message;
            ume.MessageOn = DateTime.Now;
            ume.isPrivate = false;
            ume.fromUserId = fromUserId;
            ume.toUserId = exceptThisUser;
            ume.IsRead = false;

            SaveMessageDetails(ume);

        }
        public void SendPrivateMessage(string toUserId, string message)
        {

            string fromUserId = Context.User.Identity.GetUserId();

            var toUser = OnlineUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = OnlineUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);
            var fromFullName = OnlineUsers.FirstOrDefault(x => x.ConnectionId == fromUserId).FullName;
            if (toUser != null && fromUser != null)
            {
                // send to 
                Clients.User(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, fromFullName);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, fromFullName);

                UsersEntity ue = new UsersEntity();
                ue.Id =ObjectId.Parse(fromUserId);
                List<string> PrivateMessages = new List<string>();
                PrivateMessages.Add(toUserId);


                UserMessagesEntity ume = new UserMessagesEntity();
                ume.UserId = fromUserId;
                ume.MessageId = Guid.NewGuid().ToString();
                ume.MessageDescription = message;
                ume.MessageOn = DateTime.Now;
                ume.isPrivate = true;
                ume.fromUserId = fromUserId;
                ume.toUserId = PrivateMessages;
                ume.IsRead = false;
                SaveMessageDetails(ume);

                PrivateConversationsEntity pce = new PrivateConversationsEntity();
                pce.FromUserId = ObjectId.Parse(fromUserId);
                pce.ToUserId = ObjectId.Parse(toUserId);
                pce.ConversationMessage = message;
                pce.ConversationTime = DateTime.Now;
                pce.IsRead = false;
                pce.IsDeletedMessage = false;
               
                ConversationsEntity ce = new ConversationsEntity();
                ce.FromUserId= ObjectId.Parse(fromUserId);
                ce.ToUserId= ObjectId.Parse(toUserId);
                ce.IsGroup = false;
                ce.IsDeletedConversation = false;
                ce.PrivateConversationList.Add(pce);


            }


        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = OnlineUsers.FirstOrDefault(x => x.ConnectionId == Context.User.Identity.GetUserId());
            if (item != null)
            {
                OnlineUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.Users(ConnectedUserIds).onUserDisconnected(id, item.UserName);

            }

            return base.OnDisconnected(stopCalled);
        }

        public bool SaveMessageDetails(UserMessagesEntity UserMessages)
        {
           return  _iUserMessageService.SaveUserMessages(UserMessages);
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