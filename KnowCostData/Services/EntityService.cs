using KnowCostData.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Services
{
    public abstract class EntityService<T> : IEntityService<T> where T : IMongoEntity
    {
        protected readonly MongoConnectionHandler<T> MongoConnectionHandler;

        public virtual void Create(T entity)
        {
            //// Save the entity with safe mode (WriteConcern.Acknowledged)
             this.MongoConnectionHandler.MongoCollection.InsertOne(entity);

            //if (!result.Ok)
            //{
            //    //// Something went wrong
            //}
        }

        public virtual void Delete(string id)
        {
            var builder = Builders<T>.Filter;
            var filter = builder.Eq(e => e.Id, new ObjectId(id));
             MongoConnectionHandler.MongoCollection.DeleteOne(filter);
            //var result = this.MongoConnectionHandler.MongoCollection.DeleteOne(
            //    Query<T>.EQ(e => e.Id,
            //    new ObjectId(id)),
            //    RemoveFlags.None,
            //    WriteConcern.Acknowledged);

            //if (!result.Ok)
            //{
            //    //// Something went wrong
            //}
        }

        protected EntityService()
        {
            MongoConnectionHandler = new MongoConnectionHandler<T>("");
        }

        public virtual T GetById(string id)
        {
            var builder = Builders<T>.Filter;
            var filter = builder.Eq(e => e.Id, new ObjectId(id));
            return MongoConnectionHandler.MongoCollection.Find(filter).FirstOrDefault();
        }

        public abstract void Update(T entity);
    }
}
