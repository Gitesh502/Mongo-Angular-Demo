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
                cfg.CreateMap<AspNetUsersEntity, AspNetUsers>()
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
            var listUsers = _unitOfWork.UserRepository.GetMany(Builders<AspNetUsers>.Filter.Empty);
            var document2Lookup = listUsers.AsQueryable().ToLookup(x => x.Id);
            foreach (var document1 in lstMsgs.AsQueryable())
            {
                document1.fromUser = document2Lookup[ObjectId.Parse(document1.users.Id.ToString())].FirstOrDefault();
                //yield return document1;
            }


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfile, UserProfileEntity>();
                cfg.CreateMap<AspNetUsers, AspNetUsersEntity>()
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
            var filter = builder.AnyEq(a => a.toUserId, UserId);
            var lstMsgs = _unitOfWork.UserMessageRepositroy.GetMany(filter).OrderByDescending(a=>a.MessageOn);
            var lsttoUserIds = lstMsgs.Select(a => ObjectId.Parse(a.fromUserId)).ToList();
            var listUsers = _unitOfWork.UserRepository.GetMany(Builders<AspNetUsers>.Filter.In(a => a.Id, lsttoUserIds));

            //var lstfromUserIds = lstMsgs.SelectMany(a => a.toUserId.Select(b => ObjectId.Parse(b)).ToList()).ToList();
            // var listUsers = _unitOfWork.UserRepository.GetMany(Builders<users>.Filter.In(a=>a.Id, lstfromUserIds));//AnyIn(a=>a.Id.ToString().ToList(), lstfromUserIds));
            //lstMsgs.ToList().ForEach(a => a.toUser = listUsers.ToList());//.Select(a => a.toUser.AddRange(listUsers.ToList()));

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
                cfg.CreateMap<UserMessages, UserMessagesEntity>();
                cfg.CreateMap<AspNetUsers, AspNetUsersEntity>()
                    .ForMember(a => a.UserProfile, opt => opt.MapFrom(s => s.UserProfile))
                    .ForMember(a => a.MasterConversation, opt => opt.MapFrom(s => s.MasterConversation));
                cfg.CreateMap<UserProfile, UserProfileEntity>();
                cfg.CreateMap<Conversations, ConversationsEntity>()
                  .ForMember(a => a.PrivateConversationList, opt => opt.MapFrom(s => s.PrivateConversationList))
                  .ForMember(a => a.GroupConversationList, opt => opt.MapFrom(s => s.GroupConversationList));
                cfg.CreateMap<PrivateConversations, PrivateConversationsEntity>();
                cfg.CreateMap<GroupConversations, GroupConversationsEntity>();
               
                //.ForMember(a => a.user, opt => opt.MapFrom(s => s.users));



            });
            IMapper mapper = config.CreateMapper();

            var userModel = mapper.Map<IEnumerable<UserMessages>, IEnumerable<UserMessagesEntity>>(lstMsgs);
            return userModel;
        }

    }
}
