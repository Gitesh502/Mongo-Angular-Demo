using KnowCostData;
using KnowCostData.Helper;
using BusinessEntities.BusinessEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using KnowCostData.Entity;
using AutoMapper;
using MongoDB.Bson;
using System.Data;
using System.Net;
using System.Security.Cryptography;

namespace BusinessService.Services
{


    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        public UserService()
        {
            _unitOfWork = new UnitOfWork();

        }
        public AspNetUsersEntity GetUserByEmail(string email)
        {
            var builder = Builders<AspNetUsers>.Filter;
            var filter = builder.Eq("Email", email);
            var userde = _unitOfWork.UserRepository.GetOne(filter);
            if (userde != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AspNetUsers, AspNetUsersEntity>()
                      .ForMember(a => a.UserProfile, opt => opt.MapFrom(s => s.UserProfile));
                    cfg.CreateMap<UserProfile, UserProfileEntity>();
                });
                IMapper mapper = config.CreateMapper();
                var userModel = mapper.Map<AspNetUsers, AspNetUsersEntity>(userde);
                return userModel;
            }
            return null;
        }

        public IEnumerable<AspNetUsersEntity> GetRegisteredUsers()
        {
            var builder = Builders<AspNetUsers>.Filter;
            var filter = builder.Empty;
            var listUsers = _unitOfWork.UserRepository.GetMany(filter);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AspNetUsers, AspNetUsersEntity>()
                     .ForMember(a => a.UserProfile, opt => opt.MapFrom(s => s.UserProfile));
                cfg.CreateMap<UserProfile, UserProfileEntity>();
            });
            IMapper mapper = config.CreateMapper();

            var userModel = mapper.Map<IEnumerable<AspNetUsers>, IEnumerable<AspNetUsersEntity>>(listUsers);
            return userModel;
        }
    }
}
