using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsAuditControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsAuditDto> GetAudsAuditDtoList()
        {
            return new System.Collections.Generic.List<AudsAuditDto>()
            {
                new AudsAuditDto()
                {
                    AuditId = 3,
                    AuditDateStart = DateTime.Now,
                    AuditDateEnd = DateTime.Now,
                    AuditResolutor = 100,
                    AuditResponsible = 100,
                    AuditDescription = "AdminPutTest",
                    AuditIsnotificationsent = true,
                    ApplicationId = 100,
                    StateId = 100,
                    IdType = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsAuditDto()
                {
                    AuditId = 4,
                    AuditDateStart = DateTime.Now,
                    AuditDateEnd = DateTime.Now,
                    AuditResolutor = 100,
                    AuditResponsible = 100,
                    AuditDescription = "AdminPutTest",
                    AuditIsnotificationsent = true,
                    ApplicationId = 100,
                    StateId = 100,
                    IdType = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsAuditDto GetAudsAuditDto()
        {
            return GetAudsAuditDtoList().FirstOrDefault();
        }
    }
}