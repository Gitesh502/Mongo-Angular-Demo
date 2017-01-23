using KnowCostData.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Repository.BaseRepository
{
    public interface IBaseRepositroy<T> where T : IMongoEntity
    {
        void Add(T entity);
        IEnumerable<T> GetMany(FilterDefinition<T> where);
        T GetOne(FilterDefinition<T> where);
        T GetSingleReference(MongoDBRef refObj);
        IEnumerable<T> GetManyReference(MongoDBRef refObj);
    }
}
