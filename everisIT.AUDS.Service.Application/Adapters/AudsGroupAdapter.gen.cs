using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsGroupAdapter : BaseAdapter<AudsGroupDto, AudsGroup>
    {
        public override AudsGroup Map(AudsGroupDto entityDto)
        {
            return entityDto == null ? null : new AudsGroup()
            { 
                GroupId = entityDto.GroupId, 
                GroupName = entityDto.GroupName, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsGroupDto Map(AudsGroup entity)
        {
            return entity == null ? null : new AudsGroupDto()
            { 
                GroupId = entity.GroupId, 
                GroupName = entity.GroupName, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsGroup> Map(List<AudsGroupDto> listDto)
        {
            List<AudsGroup> list = null;

            if (listDto != null)
            {
                list = new List<AudsGroup>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsGroupDto> Map(IList<AudsGroup> list)
        {
            List<AudsGroupDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsGroupDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}