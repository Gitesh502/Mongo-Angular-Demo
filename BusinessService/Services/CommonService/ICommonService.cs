using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public interface ICommonService
    {
        long GetUniqueConversationId(string fromUserId, string toUserId);
    }
}
