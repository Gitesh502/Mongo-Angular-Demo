using MongoDB.Bson;
using System;
using System.Collections.Generic;
namespace BusinessEntities.BusinessEntityModels
{

    public class ConversationEntity
    {
        public ConversationEntity()
        {
            PrivateConversationList = new List<PrivateConversationEntity>();
        }
        public long ConversationId { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDeletedConversation { get; set; }
        public ObjectId From { get; set; }
        public ObjectId To { get; set; }
        public List<PrivateConversationEntity> PrivateConversationList { get; set; }
    }

    public class PrivateConversationEntity
    {
        public ObjectId FromUserId { get; set; }
        public ObjectId ToUserId { get; set; }
        public string ConversationMessage { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeletedMessage { get; set; }
        public DateTime MessageOn { get; set; }
    }

    //public class GroupConversationsEntity
    //{
    //    public ObjectId GroupFromUserId { get; set; }
    //    public List<ObjectId> GroupUserIds { get; set; }
    //    public bool GroupIsRead { get; set; }
    //    public DateTime GroupConversationTime { get; set; }
    //    public string GroupConversationMessage { get; set; }
    //    public AspNetUsersEntity GroupFromUserDetails { get; set; }
    //    public List<AspNetUsersEntity> GroupUserDetails { get; set; }
    //    public bool IsDeletedGroupConversation { get; set; }
    //    public ObjectId GroupStartedBy { get; set; }
    //}
}
