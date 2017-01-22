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

namespace BusinessService.Services 
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
                    cfg.CreateMap<UserProfile, UserProfileEntity>();
                });
                IMapper mapper = config.CreateMapper();

                var userModel = mapper.Map<users, UsersEntity>(userde);
                return userModel;
            }
            return null;
        }

        public IEnumerable<UsersEntity> GetRegisteredUsers()
        {
            var builder = Builders<users>.Filter;
            var filter = builder.Empty;
            var listUsers = _unitOfWork.UserRepository.GetMany(filter);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<users, UsersEntity>();
                cfg.CreateMap<UserProfile, UserProfileEntity>();

            });
            IMapper mapper = config.CreateMapper();

            var userModel = mapper.Map<IEnumerable<users>, IEnumerable<UsersEntity>>(listUsers);
            return userModel;
        }
    }
}
