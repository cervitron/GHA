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
    public partial class AudsGroupService : IAudsGroupService
    {
        private readonly IAudsGroupRepository audsGroupRepository;
		private readonly IBaseAdapter<AudsGroupDto, AudsGroup> adapter;

        /// <summary>
        /// AudsGroupService constructor
        /// </summary>        
        /// <param name="_audsGroupRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsGroupService(
           IAudsGroupRepository _audsGroupRepository,
           IBaseAdapter<AudsGroupDto, AudsGroup> _adapter)
        {
			audsGroupRepository = _audsGroupRepository ?? throw new ArgumentNullException(nameof(_audsGroupRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsGroup
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsGroupDto</returns>
        public async Task<AudsGroupDto> Create(AudsGroupDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsGroupDto();
            }
            return adapter.Map(await audsGroupRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsGroups filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsGroup list</returns>
        public async Task<System.Collections.Generic.IList<AudsGroupDto>> GetList(IAudsGroupFilter filter)
        {
            return adapter.Map(await audsGroupRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsGroup Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsGroup ID</returns>
        public async Task<AudsGroupDto> Delete(int id)
        {
            return adapter.Map(await audsGroupRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsGroup to a new audsGroup object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsGroup ID</returns>
        public async Task<AudsGroupDto> Update(AudsGroupDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsGroupDto();
            }
            return adapter.Map(await audsGroupRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsGroupDto> Get(int id)
        {            
            return adapter.Map(await audsGroupRepository.Get(id));
        }
    }
}