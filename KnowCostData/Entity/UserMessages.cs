using KnowCostData.Helper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Entity
{
    [BsonIgnoreExtraElements]
    public class UserMessages : MongoEntity
    {
        public string UserId { get; set; }
        public string MessageId{get;set;}
        public string MessageDescription{get;set;}
        public bool isPrivate{get;set;}
        public string fromUserId{get;set;}
        public List<string> toUserId{get;set;}
        [BsonDateTimeOptions]
        public DateTime MessageOn{get;set;}
        public bool IsRead{get;set;}
        public users fromUser { get; set; }
        public List<users> toUser { get; set; }
        public MongoDBRef users { get; set; }
        //public users getusers(imongodatabase db)
        //{
        //    if (users == null)
        //        return new users();

        //    users tpass = new users();
        //    tpass = db.fetchdbref<users>(users);
        //    return tpass;
        //}
    }

   
}
