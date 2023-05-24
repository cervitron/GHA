using everisIT.AUDS.Service.Application.Adapters.Interfaces;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace everisIT.AUDS.Service.Application.Services
{
    public partial class AudsTypeService : IAudsTypeService
    {
        private readonly IAudsTypeRepository audsTypeRepository;
		private readonly IBaseAdapter<AudsTypeDto, AudsType> adapter;

        /// <summary>
        /// AudsTypeService constructor
        /// </summary>        
        /// <param name="_audsTypeRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsTypeService(
           IAudsTypeRepository _audsTypeRepository,
           IBaseAdapter<AudsTypeDto, AudsType> _adapter)
        {
			audsTypeRepository = _audsTypeRepository ?? throw new ArgumentNullException(nameof(_audsTypeRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsType
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsTypeDto</returns>
        public async Task<AudsTypeDto> Create(AudsTypeDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsTypeDto();
            }
            return adapter.Map(await audsTypeRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsTypes filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsType list</returns>
        public async Task<System.Collections.Generic.IList<AudsTypeDto>> GetList(IAudsTypeFilter filter)
        {
            return adapter.Map(await audsTypeRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsType Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsType ID</returns>
        public async Task<AudsTypeDto> Delete(int id)
        {
            return adapter.Map(await audsTypeRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsType to a new audsType object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsType ID</returns>
        public async Task<AudsTypeDto> Update(AudsTypeDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsTypeDto();
            }
            return adapter.Map(await audsTypeRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsTypeDto> Get(int id)
        {            
            return adapter.Map(await audsTypeRepository.Get(id));
        }
    }
}