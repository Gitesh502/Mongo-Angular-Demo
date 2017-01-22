using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BusinessEntityModels
{
    public class ConnectionMappingsEntity
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public DateTime ConnectedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
