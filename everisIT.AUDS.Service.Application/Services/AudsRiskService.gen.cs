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
    public partial class AudsRiskService : IAudsRiskService
    {
        private readonly IAudsRiskRepository audsRiskRepository;
		private readonly IBaseAdapter<AudsRiskDto, AudsRisk> adapter;

        /// <summary>
        /// AudsRiskService constructor
        /// </summary>        
        /// <param name="_audsRiskRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsRiskService(
           IAudsRiskRepository _audsRiskRepository,
           IBaseAdapter<AudsRiskDto, AudsRisk> _adapter)
        {
			audsRiskRepository = _audsRiskRepository ?? throw new ArgumentNullException(nameof(_audsRiskRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsRisk
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsRiskDto</returns>
        public async Task<AudsRiskDto> Create(AudsRiskDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsRiskDto();
            }
            return adapter.Map(await audsRiskRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsRisks filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsRisk list</returns>
        public async Task<System.Collections.Generic.IList<AudsRiskDto>> GetList(IAudsRiskFilter filter)
        {
            return adapter.Map(await audsRiskRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsRisk Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsRisk ID</returns>
        public async Task<AudsRiskDto> Delete(int id)
        {
            return adapter.Map(await audsRiskRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsRisk to a new audsRisk object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsRisk ID</returns>
        public async Task<AudsRiskDto> Update(AudsRiskDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsRiskDto();
            }
            return adapter.Map(await audsRiskRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsRiskDto> Get(int id)
        {            
            return adapter.Map(await audsRiskRepository.Get(id));
        }
    }
}