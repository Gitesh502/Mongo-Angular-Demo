using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Entity
{
    public class ConnectedUsers:MongoEntity
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        [BsonDateTimeOptions]
        public DateTime ConnectedTime { get; set; }
        public ObjectId UserID { get; set; }
    }
}
