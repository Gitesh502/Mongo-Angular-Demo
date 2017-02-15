using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnowCostWeb.Models;
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

        static List<ConversationsModel> Conversatations { get; set; }

        static List<string> ConnectedUserIds = new List<string>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        static List<ChatUtilities.ConnectedUsers> OnlineUsers = new List<ChatUtilities.ConnectedUsers>();

        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
      
      
        private readonly IUserService _iuserservices;
        private readonly ICommonService _iCommonService;
        private readonly IConversationService _iConversationService;
        public ChatAppHub(IUserService IUserServices,ICommonService ICommonService,IConversationService IConversationService)
        {
            _iuserservices = IUserServices;
            _iCommonService = ICommonService;
            _iConversationService = IConversationService;
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
            //ConnectionMappingsEntity objCom = new ConnectionMappingsEntity();
            //objCom.ConnectionId = id;
            //objCom.UserId = userId;
            //objCom.IsActive = true;
            //objCom.ConnectedOn = DateTime.Now;
            //_iConnectionMappingService.SaveConnectionMapping(objCom);
            return base.OnConnected();
        }

        #region Send Group Message
        public void SendMessageToAll(string userName, string message,string Email)
        {
            string fromUserId = Context.User.Identity.GetUserId();
            var fromFullName = OnlineUsers.FirstOrDefault(x => x.ConnectionId == fromUserId).FullName;
            AddMessageinCache(userName, message,Email);
            Clients.Users(ConnectedUserIds).messageReceived(userName, message, Email, fromFullName);

            var exceptThisUser = ConnectedUserIds.Where(a => a != fromUserId).ToList();
        }
        #endregion

        #region Send PrivateMessage
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

               

                PrivateConversationEntity PEC = new PrivateConversationEntity
                {
                    FromUserId = ObjectId.Parse(fromUserId),
                    ToUserId = ObjectId.Parse(toUserId),
                    ConversationMessage = message,
                    IsRead = false,
                    IsDeletedMessage = false,
                    MessageOn = DateTime.Now
                };
                ConversationEntity OCE = new ConversationEntity();
                {
                    OCE.ConversationId = GetConversationID(fromUserId, toUserId);
                    OCE.IsGroup = false;
                    OCE.IsDeletedConversation = false;
                    OCE.From = ObjectId.Parse(fromUserId);
                    OCE.To = ObjectId.Parse(toUserId);
                    OCE.PrivateConversationList.Add(PEC);
                };
                UpdateConversations(ObjectId.Parse(fromUserId), ObjectId.Parse(toUserId), OCE);
            }
        }
        #endregion


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

       
        public void UpdateConversations(ObjectId fromUserId,ObjectId toUserId,ConversationEntity ObjCE)
        {
            _iConversationService.UpdateConversations(fromUserId, toUserId, ObjCE);
        }
        public long GetConversationID(string fromUserId,string toUserId)
        {
            return _iCommonService.GetUniqueConversationId(fromUserId, toUserId);
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