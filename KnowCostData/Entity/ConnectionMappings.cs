using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Entity
{
    [BsonIgnoreExtraElements]
    public class ConnectionMappings : MongoEntity
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        [BsonDateTimeOptions]
        public DateTime ConnectedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
