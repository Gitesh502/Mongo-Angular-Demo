using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowCostData.Models;
using KnowCostData.Entity;
using MongoDB.Driver;

namespace KnowCostData.Repository
{
    public class ChatRepository: IChatRepository
    {
        public dynamic SaveConnectedUsers(ConnectedUsers repObj)
        {
            MongoConnectionHandler<ConnectedUsers> obj = new MongoConnectionHandler<ConnectedUsers>("ConnectedUsers");
            var builder = Builders<ConnectedUsers>.Filter;
            var filter = builder.Eq("UserName", repObj.UserName);
            var collection = obj.MongoCollection.FindAsync(filter).Result.ToList();
            if(collection.Count()>0)
            {
                obj.MongoCollection.DeleteManyAsync(filter);
            }
            var i=obj.MongoCollection.InsertOneAsync(repObj);
            return "";
        }
    }
}
