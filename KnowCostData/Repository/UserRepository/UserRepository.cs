using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowCostData.Services;
using KnowCostData.Entity;
using MongoDB.Driver;
using MongoDB.Bson;
using KnowCostData.Models;


namespace KnowCostData.Repository
{
    public class UserRepository:IUserRepository
    {
        public users GetUserByEmail(string email)
        {
            users me = new users();
            MongoConnectionHandler<users> obj= new MongoConnectionHandler<users>("users");
           
            var builder = Builders<users>.Filter;
            var filter = builder.Eq("Email", email);
            var collection = obj.MongoCollection.FindAsync(filter).Result.FirstOrDefault();
            return collection;
        }
    }
}
