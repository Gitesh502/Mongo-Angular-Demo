using System;
using MongoDB.Bson;

namespace KnowCostData.Entity
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
