using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Entity
{
    public class Conversation : MongoEntity
    {
        public Conversation()
        {
            PrivateConversationList = new List<PrivateConversation>();
        }
        public long ConversationId { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDeletedConversation { get; set; }
        public ObjectId From { get; set; }
        public ObjectId To { get; set; }
        public List<PrivateConversation> PrivateConversationList { get; set; }
    }

    public class PrivateConversation
    {
        public ObjectId FromUserId { get; set; }
        public ObjectId ToUserId { get; set; }
        public string ConversationMessage { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeletedMessage { get; set; }
        [BsonDateTimeOptions]
        public DateTime MessageOn { get; set; }
    }
    //[BsonIgnoreExtraElements]
    //public class GroupConversations : MongoEntity
    //{
    //    public ObjectId GroupFromUserId { get; set; }
    //    public List<ObjectId> GroupUserIds { get; set; }
    //    public bool GroupIsRead { get; set; }
    //    [BsonDateTimeOptions]
    //    public DateTime GroupConversationTime { get; set; }
    //    public string GroupConversationMessage { get; set; }
    //    public AspNetUsers GroupFromUserDetails { get; set; }
    //    public List<AspNetUsers> GroupUserDetails { get; set; }
    //    public bool IsDeletedMessage { get; set; }
    //    public ObjectId GroupStartedBy { get; set; }
    //}
}
