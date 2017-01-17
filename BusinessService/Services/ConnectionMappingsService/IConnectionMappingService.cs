using BusinessEntities.BusinessEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public interface IConnectionMappingService
    {
        bool SaveConnectionMapping(ConnectionMappingsEntity objConnectionMapping);
    }
}
