using AutoMapper;
using BusinessEntities.BusinessEntityModels;
using KnowCostData;
using KnowCostData.Entity;
using MongoDB.Bson;
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
                cfg.CreateMap<UserMessagesEntity, UserMessages>();
                     //.ForMember(a => a.users, opt => opt.MapFrom(s => s.user));


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
            var lstMsgs = _unitOfWork.UserMessageRepositroy.GetMany(filter);
            var listUsers = _unitOfWork.UserRepository.GetMany(Builders<users>.Filter.Empty);
            var document2Lookup = listUsers.AsQueryable().ToLookup(x => x.Id);
            foreach (var document1 in lstMsgs.AsQueryable())
            {
                document1.fromUser = document2Lookup[ObjectId.Parse(document1.users.Id.ToString())].FirstOrDefault();
                //yield return document1;
            }

         
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfile, UserProfileEntity>();
                cfg.CreateMap<users, UsersEntity>()
                     .ForMember(a => a.UserProfile, opt => opt.MapFrom(s => s.UserProfile));
                cfg.CreateMap<UserMessages, UserMessagesEntity>();
                     //.ForMember(a => a.user, opt => opt.MapFrom(s => s.users));
                   


            });
            IMapper mapper = config.CreateMapper();

            var userModel = mapper.Map<IEnumerable<UserMessages>, IEnumerable<UserMessagesEntity>>(lstMsgs);
            return userModel;
        }


        public IEnumerable<UserMessagesEntity> GetMessagesByUserId(string UserId)
        {
            var builder = Builders<UserMessages>.Filter;
            var filter = builder.Eq("toUser", UserId);
            var lstMsgs = _unitOfWork.UserMessageRepositroy.GetMany(filter);
            var listUsers = _unitOfWork.UserRepository.GetMany(Builders<users>.Filter.Empty);
            var document2Lookup = listUsers.AsQueryable().ToLookup(x => x.Id);
            foreach (var document1 in lstMsgs.AsQueryable())
            {
                document1.fromUser = document2Lookup[ObjectId.Parse(document1.fromUserId.ToString())].FirstOrDefault();
                //yield return document1;
            }

            //lstMsgs.users = new MongoDBRef("users", ObjectId.Parse(UserId));
            //lstMsgs.relusers = _unitOfWork.UserRepository.GetSingleReference(lstMsgs.users);
            //  lstMsgs.users = new MongoDBRef("users", ObjectId.Parse(UserId));

            //var listUsers = _unitOfWork.UserRepository.GetOne(Builders<users>.Filter.Eq("_id",ObjectId.Parse(UserId)));
            //lstMsgs.relusers = listUsers;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfile, UserProfileEntity>();
                cfg.CreateMap<users, UsersEntity>()
                     .ForMember(a => a.UserProfile, opt => opt.MapFrom(s => s.UserProfile));
                cfg.CreateMap<UserMessages, UserMessagesEntity>();
                     //.ForMember(a => a.user, opt => opt.MapFrom(s => s.users));



            });
            IMapper mapper = config.CreateMapper();

            var userModel = mapper.Map<IEnumerable<UserMessages>, IEnumerable<UserMessagesEntity>>(lstMsgs);
            return userModel;
        }

    }
}
