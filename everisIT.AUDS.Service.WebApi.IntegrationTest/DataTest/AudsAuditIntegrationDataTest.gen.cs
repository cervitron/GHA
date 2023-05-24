using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsAuditIntegrationDataTest
    {
        public static AudsAudit ResetEntityDto(AudsAudit entity)
        {
            return new AudsAudit()
            {
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