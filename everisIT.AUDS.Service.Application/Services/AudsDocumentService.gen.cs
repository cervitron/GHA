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
    public partial class AudsDocumentService : IAudsDocumentService
    {
        private readonly IAudsDocumentRepository audsDocumentRepository;
		private readonly IBaseAdapter<AudsDocumentDto, AudsDocument> adapter;

        /// <summary>
        /// AudsDocumentService constructor
        /// </summary>        
        /// <param name="_audsDocumentRepository"></param>        
        /// <param name="_adapter"></param>
        /// <param name="logRecordUtilityExtension"></param>
        public AudsDocumentService(
           IAudsDocumentRepository _audsDocumentRepository,
           IBaseAdapter<AudsDocumentDto, AudsDocument> _adapter)
        {
			audsDocumentRepository = _audsDocumentRepository ?? throw new ArgumentNullException(nameof(_audsDocumentRepository));
            adapter = _adapter ?? throw new ArgumentNullException(nameof(_adapter));
        }
        /// <summary>
        /// Insert an AudsDocument
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsDocumentDto</returns>
        public async Task<AudsDocumentDto> Create(AudsDocumentDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsDocumentDto();
            }
            return adapter.Map(await audsDocumentRepository.Create(adapter.Map(dataDto)));            
        }

        /// <summary>
        /// Get AudsDocuments filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsDocument list</returns>
        public async Task<System.Collections.Generic.IList<AudsDocumentDto>> GetList(IAudsDocumentFilter filter)
        {
            return adapter.Map(await audsDocumentRepository.GetList(filter));            
        }

        /// <summary>
        /// Update the AudsDocument Status to Enable or Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsDocument ID</returns>
        public async Task<AudsDocumentDto> Delete(int id)
        {
            return adapter.Map(await audsDocumentRepository.Delete(id));            
        }

        /// <summary>
        /// Update an audsDocument to a new audsDocument object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsDocument ID</returns>
        public async Task<AudsDocumentDto> Update(AudsDocumentDto dataDto)
        {            
            if (dataDto == null)
            {
                return new AudsDocumentDto();
            }
            return adapter.Map(await audsDocumentRepository.Update(adapter.Map(dataDto)));            
        }

        public async Task<AudsDocumentDto> Get(int id)
        {            
            return adapter.Map(await audsDocumentRepository.Get(id));
        }
    }
}