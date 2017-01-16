//using MongoDB.Driver;
//using System;
//using AspNet.Identity.MongoDB;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using KnowCostWeb.Models;
//using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using KnowCostWeb.Models;


namespace KnowCostWeb
{
    public class ApplicationIdentityContext : IDisposable
    {
        public static ApplicationIdentityContext Create()
        {
            // todo add settings where appropriate to switch server & database in your own application
            var client = new MongoClient("mongodb://localhost:27017");
            //var client = new MongoClient("mongodb://chatapp-mongo:0Ot0q1C69gy9JRxqgjxvddAljHL0IIVGPHGJ0wV7dsXLecytzeLOZ3alpKkWc3DWSy5I4L7wxA6ZB5XQkZorrg==@chatapp-mongo.documents.azure.com:10250/?ssl=true");
            var database = client.GetDatabase("KnowCost");
            var users = database.GetCollection<ApplicationUser>("users");
            var roles = database.GetCollection<IdentityRole>("roles");
            return new ApplicationIdentityContext(users, roles);
        }

        private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users, IMongoCollection<IdentityRole> roles)
        {
            Users = users;
            Roles = roles;
        }

        public IMongoCollection<IdentityRole> Roles { get; set; }

        public IMongoCollection<ApplicationUser> Users { get; set; }

        public Task<List<IdentityRole>> AllRolesAsync()
        {
            return Roles.Find(r => true).ToListAsync();
        }

        public void Dispose()
        {
        }
    }
}