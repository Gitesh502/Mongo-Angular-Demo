using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowCostData.Services;
using KnowCostData.Entity;
using MongoDB.Driver;
using MongoDB.Bson;

namespace KnowCostData.Repository
{
    public class UserRepository:IUserRepository
    {
        public dynamic GetUserById(string Id, string email)
        {
            //var builder = Builders<T>.Filter;
            //var filter = builder.Eq(e => e.Id, new ObjectId(Id));
            //return MongoConnectionHandler.MongoCollection.Find(filter).FirstOrDefault();
            return "";
        }
    }
}
