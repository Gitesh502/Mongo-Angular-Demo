using BusinessEntities.BusinessEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public interface IUserService
    {
        UsersEntity GetUserByEmail(string email);
       // IEnumerable<UsersEntity> GetRegisteredUsers();
    }
}
