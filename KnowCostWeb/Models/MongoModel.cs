using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace KnowCostWeb.Models
{
    public class MongoModel
    {
        public IMongoClient _client { get; set; }
        public IMongoDatabase _db { get; set; }
    }
}