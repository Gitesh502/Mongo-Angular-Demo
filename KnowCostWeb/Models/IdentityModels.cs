using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MongoDB;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using MongoDB.Bson;
//using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowCostWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("FirstName", this.FirstName.ToString()));
            userIdentity.AddClaim(new Claim("LastName", this.LastName.ToString()));
            userIdentity.AddClaim(new Claim("FullName", this.FullName.ToString()));
            // Add custom user claims here
            return userIdentity;
        }
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual List<Conversations> MasterConversation { get; set; }
       
    }
    public class UserProfile
    {
        [Key]
        public int ProfileID{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Email{get;set;}
    }
    public class Conversations
    {
        public Conversations()
        {
            PrivateConversationList = new List<PrivateConversations>();
            GroupConversationList = new List<GroupConversations>();

        }
        public string ConversationId { get; set; }
        public ObjectId FromUserId { get; set; }
        public ObjectId ToUserId { get; set; }
        public bool IsGroup { get; set; }
        public bool IsDeletedConversation { get; set; }
        public DateTime ConversationStartedOn { get; set; }
        public List<PrivateConversations> PrivateConversationList { get; set; }
        public List<GroupConversations> GroupConversationList { get; set; }
    }

    public class PrivateConversations
    {
        public ObjectId PrivateFromUserId { get; set; }
        public ObjectId PrivateToUserId { get; set; }
        public string ConversationMessage { get; set; }
        public DateTime ConversationTime { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeletedMessage { get; set; }

    }

    public class GroupConversations
    {
        public ObjectId GroupFromUserId { get; set; }
        public List<ObjectId> GroupUserIds { get; set; }
        public bool GroupIsRead { get; set; }
        public DateTime GroupConversationTime { get; set; }
        public string GroupConversationMessage { get; set; }
        public bool IsDeletedGroupConversation { get; set; }
        public ObjectId GroupStartedBy { get; set; }
    }
    public class Messages
    {
        public string fromUserName { get; set; }
        public ObjectId fromUserId { get; set; }
        public string MessageDesc { get; set; }
    }
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}