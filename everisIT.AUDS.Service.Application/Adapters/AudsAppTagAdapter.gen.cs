using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsAppTagAdapter : BaseAdapter<AudsAppTagDto, AudsAppTag>
    {
        public override AudsAppTag Map(AudsAppTagDto entityDto)
        {
            return entityDto == null ? null : new AudsAppTag()
            { 
                ApplicationId = entityDto.ApplicationId, 
                TagId = entityDto.TagId, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsAppTagDto Map(AudsAppTag entity)
        {
            return entity == null ? null : new AudsAppTagDto()
            { 
                ApplicationId = entity.ApplicationId, 
                TagId = entity.TagId, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsAppTag> Map(List<AudsAppTagDto> listDto)
        {
            List<AudsAppTag> list = null;

            if (listDto != null)
            {
                list = new List<AudsAppTag>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsAppTagDto> Map(IList<AudsAppTag> list)
        {
            List<AudsAppTagDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsAppTagDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}