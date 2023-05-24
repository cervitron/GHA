using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsRiskIntegrationDataTest
    {
        public static AudsRisk ResetEntityDto(AudsRisk entity)
        {
            return new AudsRisk()
            {
                RiskId = entity.RiskId,
                RiskName = entity.RiskName,
                HowManyDaysUntilNotification = entity.HowManyDaysUntilNotification,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}