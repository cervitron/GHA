using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsAuditResponsibleAdapter : BaseAdapter<AudsAuditResponsibleDto, AudsAuditResponsible>
    {
        public override AudsAuditResponsible Map(AudsAuditResponsibleDto entityDto)
        {
            return entityDto == null ? null : new AudsAuditResponsible()
            {
                Id = entityDto.Id,
                AuditId = entityDto.AuditId,
                IdEmployee = entityDto.IdEmployee,
                UserNewRegister = entityDto.UserNewRegister,
                DateNewRegister = entityDto.DateNewRegister,
                UserLastUpdateRegister = entityDto.UserLastUpdateRegister,
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister,
                CodeStatus = entityDto.CodeStatus
            };
        }

        public override AudsAuditResponsibleDto Map(AudsAuditResponsible entity)
        {
            return entity == null ? null : new AudsAuditResponsibleDto()
            {
                Id = entity.Id,
                AuditId = entity.AuditId,
                IdEmployee = entity.IdEmployee,
                UserNewRegister = entity.UserNewRegister,
                DateNewRegister = entity.DateNewRegister,
                UserLastUpdateRegister = entity.UserLastUpdateRegister,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }

        public override List<AudsAuditResponsible> Map(List<AudsAuditResponsibleDto> listDto)
        {
            List<AudsAuditResponsible> list = null;

            if (listDto != null)
            {
                list = new List<AudsAuditResponsible>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsAuditResponsibleDto> Map(IList<AudsAuditResponsible> list)
        {
            List<AudsAuditResponsibleDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsAuditResponsibleDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}