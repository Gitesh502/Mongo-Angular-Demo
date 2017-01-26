using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Entity
{
    [BsonIgnoreExtraElements]
    public class Conversations :MongoEntity
    {
        public ObjectId FromUserId { get; set; }
        public ObjectId ToUserId { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDeletedConversation { get; set; }
        public users FromUserDetails { get; set; }
        public users ToUserDetails { get; set; }
        [BsonDateTimeOptions]
        public DateTime ConversationStartedOn { get; set; }
        public List<PrivateConversations> PrivateConversationList { get; set; }
        public List<GroupConversations> GroupConversationList { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class PrivateConversations : MongoEntity
    {
        public ObjectId FromUserId { get; set; }
        public ObjectId ToUserId { get; set; }
        public string ConversationMessage { get; set; }
        [BsonDateTimeOptions]
        public DateTime ConversationTime { get; set; }
        public bool IsRead { get; set; }
        public users FromUserDetails { get; set; }
        public users ToUserDetails { get; set; }
        public bool IsDeletedMessage { get; set; }
        
    }
    [BsonIgnoreExtraElements]
    public class GroupConversations : MongoEntity
    {
        public ObjectId GroupFromUserId { get; set; }
        public List<ObjectId> GroupUserIds { get; set; }
        public bool GroupIsRead { get; set; }
        [BsonDateTimeOptions]
        public DateTime GroupConversationTime { get; set; }
        public string GroupConversationMessage { get; set; }
        public users GroupFromUserDetails { get; set; }
        public List<users> GroupUserDetails { get; set; }
        public bool IsDeletedMessage { get; set; }
        public ObjectId GroupStartedBy { get; set; }
    }
}
