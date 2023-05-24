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
    public partial class AudsAuditService : IAudsAuditService
    {
        private readonly IAudsAuditRepository audsAuditRepository;
		private readonly IBaseAdapter<AudsAuditDto, AudsAudit> adapter;

        /// <summary>
        /// AudsAuditService constructor
        /// </summary>        
        /// <param name="_audsAuditRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsAuditService(
           IAudsAuditRepository _audsAuditRepository,
           IBaseAdapter<AudsAuditDto, AudsAudit> _adapter)
        {
			audsAuditRepository = _audsAuditRepository ?? throw new ArgumentNullException(nameof(_audsAuditRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsAudit
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsAuditDto</returns>
        public async Task<AudsAuditDto> Create(AudsAuditDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsAuditDto();
            }
            return adapter.Map(await audsAuditRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsAudits filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsAudit list</returns>
        public async Task<System.Collections.Generic.IList<AudsAuditDto>> GetList(IAudsAuditFilter filter)
        {
            return adapter.Map(await audsAuditRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsAudit Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsAudit ID</returns>
        public async Task<AudsAuditDto> Delete(int id)
        {
            return adapter.Map(await audsAuditRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsAudit to a new audsAudit object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsAudit ID</returns>
        public async Task<AudsAuditDto> Update(AudsAuditDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsAuditDto();
            }
            return adapter.Map(await audsAuditRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsAuditDto> Get(int id)
        {            
            return adapter.Map(await audsAuditRepository.Get(id));
        }
    }
}