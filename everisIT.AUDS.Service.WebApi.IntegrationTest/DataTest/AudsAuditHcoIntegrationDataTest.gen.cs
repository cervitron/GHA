using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsAuditHcoIntegrationDataTest
    {
        public static AudsAuditHco ResetEntityDto(AudsAuditHco entity)
        {
            return new AudsAuditHco()
            {
                AuditHcoId = entity.AuditHcoId,
                AuditId = entity.AuditId,
                AuditDateStart = entity.AuditDateStart,
                AuditDateEnd = entity.AuditDateEnd,
                AuditResolutor = entity.AuditResolutor,
                AuditResponsible = entity.AuditResponsible,
                AuditDescription = entity.AuditDescription,
                AuditIsNotificationSent = entity.AuditIsNotificationSent,
                ApplicationId = entity.ApplicationId,
                StateId = entity.StateId,
                IdType = entity.IdType,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}