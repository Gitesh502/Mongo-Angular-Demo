using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessService.Helper;

namespace BusinessService.Services
{
    public class CommonService : ICommonService
    {
        public long GetUniqueConversationId(string fromUserId, string toUserId)
        {
            return UniqueIdGenerator.GenerateUniqueID(fromUserId, toUserId);
        }
    }
}
