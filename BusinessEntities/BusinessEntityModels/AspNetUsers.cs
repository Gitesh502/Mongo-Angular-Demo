using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BusinessEntityModels
{
    public class AspNetUsersEntity
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
        public UserProfileEntity UserProfile { get; set; }
    }
    public class UserProfileEntity
    {
        public int ProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
    }
}
