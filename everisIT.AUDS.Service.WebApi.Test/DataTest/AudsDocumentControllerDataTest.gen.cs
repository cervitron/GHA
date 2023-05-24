using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsDocumentControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsDocumentDto> GetAudsDocumentDtoList()
        {
            return new System.Collections.Generic.List<AudsDocumentDto>()
            {
                new AudsDocumentDto()
                {
                    DocumentId = 3,
                    DocumentName = "AdminPutTest",
                    DocumentUserUpload = 100,
                    DocumentDateUpload = DateTime.Now,
                    DocumentDescription = "AdminPutTest",
                    AuditId = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsDocumentDto()
                {
                    DocumentId = 4,
                    DocumentName = "AdminPutTest",
                    DocumentUserUpload = 100,
                    DocumentDateUpload = DateTime.Now,
                    DocumentDescription = "AdminPutTest",
                    AuditId = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsDocumentDto GetAudsDocumentDto()
        {
            return GetAudsDocumentDtoList().FirstOrDefault();
        }
    }
}