using AutoMapper;
using BusinessEntities.BusinessEntityModels;
using KnowCostData;
using KnowCostData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class ConnectionMappingService : IConnectionMappingService
    {
        private readonly UnitOfWork _unitOfWork;
        public ConnectionMappingService()
        {
            _unitOfWork = new UnitOfWork();

        }

        public bool SaveConnectionMapping(ConnectionMappingsEntity objConnectionMapping)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ConnectionMappingsEntity, ConnectionMappings>();
            });
            IMapper mapper = config.CreateMapper();
            var mappingModel = mapper.Map<ConnectionMappingsEntity, ConnectionMappings>(objConnectionMapping);
            _unitOfWork.ConnectionMappingRepository.Add(mappingModel);
            return true;
        }
    }
}
