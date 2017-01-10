﻿using MongoDB.Driver;
namespace KnowCostData.Entity
{
    public class MongoConnectionHandler<T> where T : IMongoEntity
    {
        public IMongoCollection<T> MongoCollection { get; private set; }

        public MongoConnectionHandler(string Collection)
        {
            const string connectionString = "mongodb://localhost:27017";

            //// Get a thread-safe client object by using a connection string
            var mongoClient = new MongoClient(connectionString);

            //// Get a reference to a server object from the Mongo client object
            const string databaseName = "KnowCost";
            var mongoServer = mongoClient.GetDatabase(databaseName);

            //// Get a reference to the "retrogamesweb" database object 
            //// from the Mongo server object
           
            var db = mongoServer;

            //// Get a reference to the collection object from the Mongo database object
            //// The collection name is the type converted to lowercase + "s"
            MongoCollection = db.GetCollection<T>(Collection) ;
        }

    }
}
