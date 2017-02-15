using BusinessEntities.BusinessEntityModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public interface IConversationService
    {
        void UpdateConversations(ObjectId fromUserId, ObjectId toUserId, ConversationEntity objCE);
        ConversationEntity GetPreviousMessagesByConversationId(string fromUserId, string toUserId, long ConversationId);
        List<ConversationEntity> GetPreviousMessagesByUserId(string toUserId);
    }
}
