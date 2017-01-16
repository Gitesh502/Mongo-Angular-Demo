using KnowCostData;
using BusinessEntities.BusinessEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using KnowCostData.Entity;
using AutoMapper;

namespace BusinessServices.Services 
{
   

    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        public UserService()
        {
            _unitOfWork=new UnitOfWork();

        }

        public UsersEntity GetUserByEmail(string email)
        {
            var builder = Builders<users>.Filter;
            var filter = builder.Eq("Email",email);
            var userde = _unitOfWork.UserRepository.GetOne(filter);
            if (userde != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<users, UsersEntity>();
                });
                IMapper mapper = config.CreateMapper();
                var userModel = mapper.Map<users, UsersEntity>(userde);
                return userModel;
            }
            return null;
        }
    }
}
