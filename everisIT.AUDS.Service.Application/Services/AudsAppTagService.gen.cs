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
    public partial class AudsAppTagService : IAudsAppTagService
    {
        private readonly IAudsAppTagRepository audsAppTagRepository;
		private readonly IBaseAdapter<AudsAppTagDto, AudsAppTag> adapter;

        /// <summary>
        /// AudsAppTagService constructor
        /// </summary>        
        /// <param name="_audsAppTagRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsAppTagService(
           IAudsAppTagRepository _audsAppTagRepository,
           IBaseAdapter<AudsAppTagDto, AudsAppTag> _adapter)
        {
			audsAppTagRepository = _audsAppTagRepository ?? throw new ArgumentNullException(nameof(_audsAppTagRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsAppTag
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsAppTagDto</returns>
        public async Task<AudsAppTagDto> Create(AudsAppTagDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsAppTagDto();
            }
            return adapter.Map(await audsAppTagRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsAppTags filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsAppTag list</returns>
        public async Task<System.Collections.Generic.IList<AudsAppTagDto>> GetList(IAudsAppTagFilter filter)
        {
            return adapter.Map(await audsAppTagRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsAppTag Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsAppTag ID</returns>
        public async Task<AudsAppTagDto> Delete(int id)
        {
            return adapter.Map(await audsAppTagRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsAppTag to a new audsAppTag object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsAppTag ID</returns>
        public async Task<AudsAppTagDto> Update(AudsAppTagDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsAppTagDto();
            }
            return adapter.Map(await audsAppTagRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsAppTagDto> Get(int id)
        {            
            return adapter.Map(await audsAppTagRepository.Get(id));
        }
    }
}