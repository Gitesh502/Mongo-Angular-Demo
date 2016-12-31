
using KnowCostWeb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowCostWeb.MongoUtilities
{
    public class MongoConnections
    {
        public static MongoModel Connect()
        {
            MongoModel connect = new MongoModel();
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("KnowCost");
            connect._db = db;
            return connect;
        }
    }
}