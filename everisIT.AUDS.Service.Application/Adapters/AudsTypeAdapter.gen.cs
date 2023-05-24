using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsTypeAdapter : BaseAdapter<AudsTypeDto, AudsType>
    {
        public override AudsType Map(AudsTypeDto entityDto)
        {
            return entityDto == null ? null : new AudsType()
            { 
                IdType = entityDto.IdType, 
                NameType = entityDto.NameType, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsTypeDto Map(AudsType entity)
        {
            return entity == null ? null : new AudsTypeDto()
            { 
                IdType = entity.IdType, 
                NameType = entity.NameType, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsType> Map(List<AudsTypeDto> listDto)
        {
            List<AudsType> list = null;

            if (listDto != null)
            {
                list = new List<AudsType>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsTypeDto> Map(IList<AudsType> list)
        {
            List<AudsTypeDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsTypeDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}