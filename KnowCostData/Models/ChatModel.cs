using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Models
{
    public class ChatModel
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public UserDetailsDataModel users { get; set;}
    }
}
