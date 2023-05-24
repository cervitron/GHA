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
    public partial class AudsAuditHcoService : IAudsAuditHcoService
    {
        private readonly IAudsAuditHcoRepository audsAuditHcoRepository;
		private readonly IBaseAdapter<AudsAuditHcoDto, AudsAuditHco> adapter;

        /// <summary>
        /// AudsAuditHcoService constructor
        /// </summary>        
        /// <param name="_audsAuditHcoRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsAuditHcoService(
           IAudsAuditHcoRepository _audsAuditHcoRepository,
           IBaseAdapter<AudsAuditHcoDto, AudsAuditHco> _adapter)
        {
			audsAuditHcoRepository = _audsAuditHcoRepository ?? throw new ArgumentNullException(nameof(_audsAuditHcoRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsAuditHco
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsAuditHcoDto</returns>
        public async Task<AudsAuditHcoDto> Create(AudsAuditHcoDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsAuditHcoDto();
            }
            return adapter.Map(await audsAuditHcoRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsAuditHcos filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsAuditHco list</returns>
        public async Task<System.Collections.Generic.IList<AudsAuditHcoDto>> GetList(IAudsAuditHcoFilter filter)
        {
            return adapter.Map(await audsAuditHcoRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsAuditHco Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsAuditHco ID</returns>
        public async Task<AudsAuditHcoDto> Delete(int id)
        {
            return adapter.Map(await audsAuditHcoRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsAuditHco to a new audsAuditHco object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsAuditHco ID</returns>
        public async Task<AudsAuditHcoDto> Update(AudsAuditHcoDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsAuditHcoDto();
            }
            return adapter.Map(await audsAuditHcoRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsAuditHcoDto> Get(int id)
        {            
            return adapter.Map(await audsAuditHcoRepository.Get(id));
        }
    }
}