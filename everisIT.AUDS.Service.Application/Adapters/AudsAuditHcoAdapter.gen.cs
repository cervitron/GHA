using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsAuditHcoAdapter : BaseAdapter<AudsAuditHcoDto, AudsAuditHco>
    {
        public override AudsAuditHco Map(AudsAuditHcoDto entityDto)
        {
            return entityDto == null ? null : new AudsAuditHco()
            { 
                AuditHcoId = entityDto.AuditHcoId, 
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
            };
        }

        public override AudsAuditHcoDto Map(AudsAuditHco entity)
        {
            return entity == null ? null : new AudsAuditHcoDto()
            { 
                AuditHcoId = entity.AuditHcoId, 
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
            };
        }

        public override List<AudsAuditHco> Map(List<AudsAuditHcoDto> listDto)
        {
            List<AudsAuditHco> list = null;

            if (listDto != null)
            {
                list = new List<AudsAuditHco>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsAuditHcoDto> Map(IList<AudsAuditHco> list)
        {
            List<AudsAuditHcoDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsAuditHcoDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}