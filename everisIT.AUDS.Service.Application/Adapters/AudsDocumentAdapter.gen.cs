using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsDocumentAdapter : BaseAdapter<AudsDocumentDto, AudsDocument>
    {
        public override AudsDocument Map(AudsDocumentDto entityDto)
        {
            return entityDto == null ? null : new AudsDocument()
            { 
                DocumentId = entityDto.DocumentId, 
                DocumentName = entityDto.DocumentName, 
                DocumentUserUpload = entityDto.DocumentUserUpload, 
                DocumentDateUpload = entityDto.DocumentDateUpload, 
                DocumentDescription = entityDto.DocumentDescription, 
                AuditId = entityDto.AuditId, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsDocumentDto Map(AudsDocument entity)
        {
            return entity == null ? null : new AudsDocumentDto()
            { 
                DocumentId = entity.DocumentId, 
                DocumentName = entity.DocumentName, 
                DocumentUserUpload = entity.DocumentUserUpload, 
                DocumentDateUpload = entity.DocumentDateUpload, 
                DocumentDescription = entity.DocumentDescription, 
                AuditId = entity.AuditId, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsDocument> Map(List<AudsDocumentDto> listDto)
        {
            List<AudsDocument> list = null;

            if (listDto != null)
            {
                list = new List<AudsDocument>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsDocumentDto> Map(IList<AudsDocument> list)
        {
            List<AudsDocumentDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsDocumentDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}