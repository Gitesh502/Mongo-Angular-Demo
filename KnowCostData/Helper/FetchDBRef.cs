using KnowCostData.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Helper
{
    public   class FetchDBRef
    {
       
    }
    public static class MessengerExtensions 
    {
        public static async Task<T> FetchDBRefAsync<T>(this IMongoDatabase database, MongoDBRef reference) where T : MongoEntity
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id.ToString(), reference.Id.AsString);
            return await database.GetCollection<T>(reference.CollectionName).Find(filter).FirstOrDefaultAsync();
        }
        public static T FetchDBRef<T>(this IMongoDatabase database, MongoDBRef reference) where T : MongoEntity
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, ObjectId.Parse(reference.Id.AsString));
            return database.GetCollection<T>(reference.CollectionName).Find(filter).FirstOrDefault();
        }
    }
}
