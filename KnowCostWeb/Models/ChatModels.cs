using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowCostWeb.Models
{
    public class AspNetUsersModel
    {
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public UserProfileModel UserProfile { get; set; }
        public List<ConversationsModel> MasterConversation { get; set; }
    }
    public class UserProfileModel
    {
        public int ProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
    }
    public class ConversationsModel
    {
        public ConversationsModel()
        {
            PrivateConversationList = new List<PrivateConversationsModel>();
            GroupConversationList = new List<GroupConversationsModel>();

        }
        public string ConversationId { get; set; }
        public ObjectId FromUserId { get; set; }
        public ObjectId ToUserId { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDeletedConversation { get; set; }
        public DateTime ConversationStartedOn { get; set; }
        public List<PrivateConversationsModel> PrivateConversationList { get; set; }
        public List<GroupConversationsModel> GroupConversationList { get; set; }
    }

    public class PrivateConversationsModel
    {
        public ObjectId PrivateFromUserId { get; set; }
        public ObjectId PrivateToUserId { get; set; }
        public string ConversationMessage { get; set; }
        public DateTime ConversationTime { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeletedMessage { get; set; }

    }

    public class GroupConversationsModel
    {
        public ObjectId GroupFromUserId { get; set; }
        public List<ObjectId> GroupUserIds { get; set; }
        public bool GroupIsRead { get; set; }
        public DateTime GroupConversationTime { get; set; }
        public string GroupConversationMessage { get; set; }
        public bool IsDeletedGroupConversation { get; set; }
        public ObjectId GroupStartedBy { get; set; }
    }
   

}