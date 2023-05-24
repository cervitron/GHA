using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsAuditHcoControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsAuditHcoDto> GetAudsAuditHcoDtoList()
        {
            return new System.Collections.Generic.List<AudsAuditHcoDto>()
            {
                new AudsAuditHcoDto()
                {
                    AuditHcoId = 3,
                    AuditId = 100,
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
                new AudsAuditHcoDto()
                {
                    AuditHcoId = 4,
                    AuditId = 100,
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

        public static AudsAuditHcoDto GetAudsAuditHcoDto()
        {
            return GetAudsAuditHcoDtoList().FirstOrDefault();
        }
    }
}