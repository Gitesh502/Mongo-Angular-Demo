using KnowCostData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Repository
{
    public interface IUserRepository
    {
        users GetUserByEmail(string email);
             
    }
}
