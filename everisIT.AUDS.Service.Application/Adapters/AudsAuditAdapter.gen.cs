using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsAuditAdapter : BaseAdapter<AudsAuditDto, AudsAudit>
    {
        public override AudsAudit Map(AudsAuditDto entityDto)
        {
            return entityDto == null ? null : new AudsAudit()
            { 
                AuditId = entityDto.AuditId, 
                AuditDateStart = entityDto.AuditDateStart, 
                AuditDateEnd = entityDto.AuditDateEnd, 
                AuditResolutor = entityDto.AuditResolutor, 
                AuditResponsible = entityDto.AuditResponsible, 
                AuditDescription = entityDto.AuditDescription,
                AuditIsNotificationSent = entityDto.AuditIsnotificationsent, 
                ApplicationId = entityDto.ApplicationId, 
                StateId = entityDto.StateId, 
                IdType = entityDto.IdType, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus,
                AuditIsNotificationEndSent = entityDto.AuditIsNotificationEndSent
            };
        }

        public override AudsAuditDto Map(AudsAudit entity)
        {
            return entity == null ? null : new AudsAuditDto()
            { 
                AuditId = entity.AuditId, 
                AuditDateStart = entity.AuditDateStart, 
                AuditDateEnd = entity.AuditDateEnd, 
                AuditResolutor = entity.AuditResolutor, 
                AuditResponsible = entity.AuditResponsible, 
                AuditDescription = entity.AuditDescription, 
                AuditIsnotificationsent = entity.AuditIsNotificationSent, 
                ApplicationId = entity.ApplicationId, 
                StateId = entity.StateId, 
                IdType = entity.IdType, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus,
                AuditIsNotificationEndSent = entity.AuditIsNotificationEndSent
            };
        }

        public override List<AudsAudit> Map(List<AudsAuditDto> listDto)
        {
            List<AudsAudit> list = null;

            if (listDto != null)
            {
                list = new List<AudsAudit>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsAuditDto> Map(IList<AudsAudit> list)
        {
            List<AudsAuditDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsAuditDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}