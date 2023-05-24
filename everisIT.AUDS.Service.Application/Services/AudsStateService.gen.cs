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
    public partial class AudsStateService : IAudsStateService
    {
        private readonly IAudsStateRepository audsStateRepository;
		private readonly IBaseAdapter<AudsStateDto, AudsState> adapter;

        /// <summary>
        /// AudsStateService constructor
        /// </summary>        
        /// <param name="_audsStateRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsStateService(
           IAudsStateRepository _audsStateRepository,
           IBaseAdapter<AudsStateDto, AudsState> _adapter)
        {
			audsStateRepository = _audsStateRepository ?? throw new ArgumentNullException(nameof(_audsStateRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsState
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsStateDto</returns>
        public async Task<AudsStateDto> Create(AudsStateDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsStateDto();
            }
            return adapter.Map(await audsStateRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsStates filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsState list</returns>
        public async Task<System.Collections.Generic.IList<AudsStateDto>> GetList(IAudsStateFilter filter)
        {
            return adapter.Map(await audsStateRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsState Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsState ID</returns>
        public async Task<AudsStateDto> Delete(int id)
        {
            return adapter.Map(await audsStateRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsState to a new audsState object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsState ID</returns>
        public async Task<AudsStateDto> Update(AudsStateDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsStateDto();
            }
            return adapter.Map(await audsStateRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsStateDto> Get(int id)
        {            
            return adapter.Map(await audsStateRepository.Get(id));
        }
    }
}