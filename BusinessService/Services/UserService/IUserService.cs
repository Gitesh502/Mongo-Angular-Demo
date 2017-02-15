using BusinessEntities.BusinessEntityModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public interface IUserService
    {
        AspNetUsersEntity GetUserByEmail(string email);
        IEnumerable<AspNetUsersEntity> GetRegisteredUsers();
    }
}
