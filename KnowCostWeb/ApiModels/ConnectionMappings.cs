using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowCostWeb.ApiModels
{
    public class ConnectionMappings
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public DateTime ConnectedOn { get; set; }
        public bool IsActive { get; set; }
    }
}