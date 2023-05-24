using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsRiskControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsRiskDto> GetAudsRiskDtoList()
        {
            return new System.Collections.Generic.List<AudsRiskDto>()
            {
                new AudsRiskDto()
                {
                    RiskId = 3,
                    RiskName = "AdminPutTest",
                    HowManyDaysUntilNotification = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsRiskDto()
                {
                    RiskId = 4,
                    RiskName = "AdminPutTest",
                    HowManyDaysUntilNotification = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsRiskDto GetAudsRiskDto()
        {
            return GetAudsRiskDtoList().FirstOrDefault();
        }
    }
}