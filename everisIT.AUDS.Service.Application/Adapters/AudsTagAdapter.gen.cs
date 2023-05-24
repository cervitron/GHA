using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsTagAdapter : BaseAdapter<AudsTagDto, AudsTag>
    {
        public override AudsTag Map(AudsTagDto entityDto)
        {
            return entityDto == null ? null : new AudsTag()
            { 
                TagId = entityDto.TagId, 
                TagName = entityDto.TagName, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsTagDto Map(AudsTag entity)
        {
            return entity == null ? null : new AudsTagDto()
            { 
                TagId = entity.TagId, 
                TagName = entity.TagName, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsTag> Map(List<AudsTagDto> listDto)
        {
            List<AudsTag> list = null;

            if (listDto != null)
            {
                list = new List<AudsTag>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsTagDto> Map(IList<AudsTag> list)
        {
            List<AudsTagDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsTagDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}