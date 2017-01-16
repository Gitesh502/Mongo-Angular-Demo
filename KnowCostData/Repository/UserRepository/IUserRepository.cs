using KnowCostData.Entity;
using KnowCostData.Repository.BaseRepository;
using KnowCostData.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Repository
{
    public interface IUserRepository: IBaseRepositroy<users>
    {
        users GetUserByEmail(string email);
        ListResponseDTO<users> GetRegisteredUsers();
    }
}
