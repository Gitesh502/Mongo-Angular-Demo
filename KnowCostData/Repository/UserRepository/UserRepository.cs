using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowCostData.Services;
using KnowCostData.Entity;
using MongoDB.Driver;
using MongoDB.Bson;
using KnowCostData.Models;
using KnowCostData.ResponseDTO;
using KnowCostData.Repository.BaseRepository;

namespace KnowCostData.Repository
{
    public class UserRpository:BaseRepository<users>, IUserRepository
    {
        public users GetUserByEmail(string email)
        {
            users me = new users();
            MongoConnectionHandler<users> obj= new MongoConnectionHandler<users>("users");
           
            var builder = Builders<users>.Filter;
            var filter = builder.Eq("Email", email);
            var collection = obj.MongoCollection.FindAsync(filter).Result.FirstOrDefault();
            return collection;
        }
        public ListResponseDTO<users> GetRegisteredUsers()
        {
            

            ListResponseDTO<users> Response = new ListResponseDTO<users>();
            MongoConnectionHandler<users> obj = new MongoConnectionHandler<users>("users");
            var result = obj.MongoCollection.FindAsync(_ => true).Result.ToList();
            if(result.Count()>0)
            {
                Response.isValid = true;
                Response.Items = result;
            }

            return Response;
        }
       
    }
}
