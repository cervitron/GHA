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
    public partial class AudsTagService : IAudsTagService
    {
        private readonly IAudsTagRepository audsTagRepository;
		private readonly IBaseAdapter<AudsTagDto, AudsTag> adapter;

        /// <summary>
        /// AudsTagService constructor
        /// </summary>        
        /// <param name="_audsTagRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsTagService(
           IAudsTagRepository _audsTagRepository,
           IBaseAdapter<AudsTagDto, AudsTag> _adapter)
        {
			audsTagRepository = _audsTagRepository ?? throw new ArgumentNullException(nameof(_audsTagRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsTag
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsTagDto</returns>
        public async Task<AudsTagDto> Create(AudsTagDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsTagDto();
            }
            return adapter.Map(await audsTagRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsTags filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsTag list</returns>
        public async Task<System.Collections.Generic.IList<AudsTagDto>> GetList(IAudsTagFilter filter)
        {
            return adapter.Map(await audsTagRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsTag Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsTag ID</returns>
        public async Task<AudsTagDto> Delete(int id)
        {
            return adapter.Map(await audsTagRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsTag to a new audsTag object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsTag ID</returns>
        public async Task<AudsTagDto> Update(AudsTagDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsTagDto();
            }
            return adapter.Map(await audsTagRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsTagDto> Get(int id)
        {            
            return adapter.Map(await audsTagRepository.Get(id));
        }
    }
}