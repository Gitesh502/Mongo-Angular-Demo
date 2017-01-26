using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BusinessEntityModels
{
    
    public class ConversationsEntity 
    {
        public ObjectId FromUserId { get; set; }
        public ObjectId ToUserId { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDeletedConversation { get; set; }
        public UsersEntity FromUserDetails { get; set; }
        public UsersEntity ToUserDetails { get; set; }
        public DateTime ConversationStartedOn { get; set; }
        public List<PrivateConversationsEntity> PrivateConversationList { get; set; }
        public List<GroupConversationsEntity> GroupConversationList { get; set; }
    }
   
    public class PrivateConversationsEntity
    {
        public ObjectId FromUserId { get; set; }
        public ObjectId ToUserId { get; set; }
        public string ConversationMessage { get; set; }
        public DateTime ConversationTime { get; set; }
        public bool IsRead { get; set; }
        public UsersEntity FromUserDetails { get; set; }
        public UsersEntity ToUserDetails { get; set; }
        public bool IsDeletedMessage { get; set; }
        
    }
   
    public class GroupConversationsEntity
    {
        public ObjectId GroupFromUserId { get; set; }
        public List<ObjectId> GroupUserIds { get; set; }
        public bool GroupIsRead { get; set; }
        public DateTime GroupConversationTime { get; set; }
        public string GroupConversationMessage { get; set; }
        public UsersEntity GroupFromUserDetails { get; set; }
        public List<UsersEntity> GroupUserDetails { get; set; }
        public bool IsDeletedGroupConversation { get; set; }
        public ObjectId GroupStartedBy { get; set; }
    }
}
