using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.Entity
{
    public interface IAspNetUsers
    {
         ObjectId Id { get; set; }
         string UserName { get; set; }
         string SecurityStamp { get; set; }
         string Email { get; set; }
         bool EmailConfirmed { get; set; }
         string PhoneNumber { get; set; }
         bool PhoneNumberConfirmed { get; set; }
         bool TwoFactorEnabled { get; set; }
    }
}
