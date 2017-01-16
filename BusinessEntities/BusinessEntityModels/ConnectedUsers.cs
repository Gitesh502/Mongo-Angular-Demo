using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BusinessEntityModels
{
    public class ConnectedUsers
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime ConnectedTime { get; set; }
        public ObjectId UserID { get; set; }
        public string IsActive { get; set; }
        public TimeSpan IdleTime { get; set; }
    }
}
