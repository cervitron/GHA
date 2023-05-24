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
    public partial class AudsApplicationService : IAudsApplicationService
    {
        private readonly IAudsApplicationRepository audsApplicationRepository;
		private readonly IBaseAdapter<AudsApplicationDto, AudsApplication> adapter;

        /// <summary>
        /// AudsApplicationService constructor
        /// </summary>        
        /// <param name="_audsApplicationRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsApplicationService(
           IAudsApplicationRepository _audsApplicationRepository,
           IBaseAdapter<AudsApplicationDto, AudsApplication> _adapter)
        {
			audsApplicationRepository = _audsApplicationRepository ?? throw new ArgumentNullException(nameof(_audsApplicationRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsApplication
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsApplicationDto</returns>
        public async Task<AudsApplicationDto> Create(AudsApplicationDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsApplicationDto();
            }
            return adapter.Map(await audsApplicationRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsApplications filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsApplication list</returns>
        public async Task<System.Collections.Generic.IList<AudsApplicationDto>> GetList(IAudsApplicationFilter filter)
        {
            return adapter.Map(await audsApplicationRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsApplication Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsApplication ID</returns>
        public async Task<AudsApplicationDto> Delete(int id)
        {
            return adapter.Map(await audsApplicationRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsApplication to a new audsApplication object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsApplication ID</returns>
        public async Task<AudsApplicationDto> Update(AudsApplicationDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsApplicationDto();
            }
            return adapter.Map(await audsApplicationRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsApplicationDto> Get(int id)
        {            
            return adapter.Map(await audsApplicationRepository.Get(id));
        }
    }
}