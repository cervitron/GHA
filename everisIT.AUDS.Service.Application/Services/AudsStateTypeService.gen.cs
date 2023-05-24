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
    public partial class AudsStateTypeService : IAudsStateTypeService
    {
        private readonly IAudsStateTypeRepository audsStateTypeRepository;
		private readonly IBaseAdapter<AudsStateTypeDto, AudsStateType> adapter;

        /// <summary>
        /// AudsStateTypeService constructor
        /// </summary>        
        /// <param name="_audsStateTypeRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsStateTypeService(
           IAudsStateTypeRepository _audsStateTypeRepository,
           IBaseAdapter<AudsStateTypeDto, AudsStateType> _adapter)
        {
			audsStateTypeRepository = _audsStateTypeRepository ?? throw new ArgumentNullException(nameof(_audsStateTypeRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsStateType
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsStateTypeDto</returns>
        public async Task<AudsStateTypeDto> Create(AudsStateTypeDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsStateTypeDto();
            }
            return adapter.Map(await audsStateTypeRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsStateTypes filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsStateType list</returns>
        public async Task<System.Collections.Generic.IList<AudsStateTypeDto>> GetList(IAudsStateTypeFilter filter)
        {
            return adapter.Map(await audsStateTypeRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsStateType Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsStateType ID</returns>
        public async Task<AudsStateTypeDto> Delete(int id)
        {
            return adapter.Map(await audsStateTypeRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsStateType to a new audsStateType object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsStateType ID</returns>
        public async Task<AudsStateTypeDto> Update(AudsStateTypeDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsStateTypeDto();
            }
            return adapter.Map(await audsStateTypeRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsStateTypeDto> Get(int id)
        {            
            return adapter.Map(await audsStateTypeRepository.Get(id));
        }
    }
}