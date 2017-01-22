using AutoMapper;
using BusinessEntities.BusinessEntityModels;
using KnowCostData;
using KnowCostData.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly UnitOfWork _unitOfWork;
        MongoClient _db;
        public UserMessageService()
        {
            _unitOfWork = new UnitOfWork();
            _db = new MongoClient();
          
        }
        public bool SaveUserMessages(UserMessagesEntity UserMessages)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfileEntity, UserProfile>();
                cfg.CreateMap<UsersEntity, users>()
               .ForMember(a => a.UserProfile, opt => opt.MapFrom(s => s.UserProfile));
                cfg.CreateMap<UserMessagesEntity, UserMessages>()
                     .ForMember(a => a.users, opt => opt.MapFrom(s => s.user));


            });
            IMapper mapper = config.CreateMapper();
            var mappingModel = mapper.Map<UserMessagesEntity, UserMessages>(UserMessages);
            _unitOfWork.UserMessageRepositroy.Add(mappingModel);
            return true;
        }
        public IEnumerable<UserMessagesEntity> GetMessages()
        {
            var builder = Builders<UserMessages>.Filter;
            var filter = builder.Empty;
            var listUsers = _unitOfWork.UserMessageRepositroy.GetMany(filter);

            var k = listUsers.Select(a => a.GetUsers(_db.GetDatabase("KnowCost"))).ToList();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfile, UserProfileEntity>();
                cfg.CreateMap<users, UsersEntity>()
                     .ForMember(a => a.UserProfile, opt => opt.MapFrom(s => s.UserProfile));
                cfg.CreateMap<UserMessages, UserMessagesEntity>()
                     .ForMember(a => a.user, opt => opt.MapFrom(s => s.users));
                   


            });
            IMapper mapper = config.CreateMapper();

            var userModel = mapper.Map<IEnumerable<UserMessages>, IEnumerable<UserMessagesEntity>>(listUsers);
            return userModel;
        }

    }
}
