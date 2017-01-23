using KnowCostData.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Repository.BaseRepository
{
    public  class BaseRepository<T> where T : IMongoEntity
    {
        MongoConnectionHandler<T> obj = new MongoConnectionHandler<T>(typeof(T).Name);
        protected T EFactory
        {
            get;
            private set;
        }

        public virtual void Add(T entity)
        {
            obj = new MongoConnectionHandler<T>(typeof(T).Name);
            obj.MongoCollection.InsertOneAsync(entity);
        }
        public virtual void AddMany(List<T> entity)
        {
            obj = new MongoConnectionHandler<T>(typeof(T).Name);
            obj.MongoCollection.InsertManyAsync(entity);
        }
        public virtual IEnumerable<T> GetMany(FilterDefinition<T> where)
        {
            obj = new MongoConnectionHandler<T>(typeof(T).Name);
            return obj.MongoCollection.FindAsync(where).Result.ToList();
        }
        public virtual T GetOne(FilterDefinition<T> where)
        {
            obj = new MongoConnectionHandler<T>(typeof(T).Name);
            return obj.MongoCollection.FindAsync(where).Result.FirstOrDefault();
        }
        public virtual T GetSingleReference(MongoDBRef refObj)
        {
            obj = new MongoConnectionHandler<T>(refObj.CollectionName);
            return obj.MongoCollection.FindAsync(Builders<T>.Filter.Eq(e=>e.Id, refObj.Id.AsObjectId)).Result.FirstOrDefault();
        }
        public virtual IEnumerable<T> GetManyReference(MongoDBRef refObj)
        {
            obj = new MongoConnectionHandler<T>(refObj.CollectionName);
            return obj.MongoCollection.FindAsync(Builders<T>.Filter.Eq("_id", ObjectId.Parse(refObj.Id.AsString))).Result.ToList();
        }


    }
}
