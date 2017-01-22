using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BusinessEntityModels
{
    
    public class UserMessagesEntity 
    {
        public string UserId { get; set; }
        public string MessageId{get;set;}
        public string MessageDescription{get;set;}
        public bool isPrivate{get;set;}
        public string fromUserId{get;set;}
        public List<string> toUserId { get; set; }
        public DateTime MessageOn{get;set;}
        public bool IsRead{get;set;}
        public MongoDBRef user { get; set; }
        public UsersEntity relusers { get; set; }
    }

   
}
