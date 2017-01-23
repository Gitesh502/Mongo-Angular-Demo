using BusinessEntities.BusinessEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public interface IUserMessageService 
    {
        bool SaveUserMessages(UserMessagesEntity UserMessages);
        IEnumerable<UserMessagesEntity> GetMessages();
        IEnumerable<UserMessagesEntity> GetMessagesByUserId(string UserId);
    }
}
