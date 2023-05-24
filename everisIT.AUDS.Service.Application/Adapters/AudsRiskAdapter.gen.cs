using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsRiskAdapter : BaseAdapter<AudsRiskDto, AudsRisk>
    {
        public override AudsRisk Map(AudsRiskDto entityDto)
        {
            return entityDto == null ? null : new AudsRisk()
            { 
                RiskId = entityDto.RiskId, 
                RiskName = entityDto.RiskName, 
                HowManyDaysUntilNotification = entityDto.HowManyDaysUntilNotification, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsRiskDto Map(AudsRisk entity)
        {
            return entity == null ? null : new AudsRiskDto()
            { 
                RiskId = entity.RiskId, 
                RiskName = entity.RiskName, 
                HowManyDaysUntilNotification = entity.HowManyDaysUntilNotification, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsRisk> Map(List<AudsRiskDto> listDto)
        {
            List<AudsRisk> list = null;

            if (listDto != null)
            {
                list = new List<AudsRisk>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsRiskDto> Map(IList<AudsRisk> list)
        {
            List<AudsRiskDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsRiskDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}