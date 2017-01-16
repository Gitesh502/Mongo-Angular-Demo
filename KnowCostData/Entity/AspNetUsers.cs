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
    public class users:MongoEntity
    {
       
        public string UserName { get; set; }
        public string SecurityStamp{get;set;}
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public UserProfile UserProfile { get; set; }
       
    }

    public class UserProfile: MongoEntity
    {
        public int ProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
    }
}
