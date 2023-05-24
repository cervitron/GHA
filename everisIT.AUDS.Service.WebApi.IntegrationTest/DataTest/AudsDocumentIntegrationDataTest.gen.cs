using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsDocumentIntegrationDataTest
    {
        public static AudsDocument ResetEntityDto(AudsDocument entity)
        {
            return new AudsDocument()
            {
                DocumentId = entity.DocumentId,
                DocumentName = entity.DocumentName,
                DocumentUserUpload = entity.DocumentUserUpload,
                DocumentDateUpload = entity.DocumentDateUpload,
                DocumentDescription = entity.DocumentDescription,
                AuditId = entity.AuditId,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}