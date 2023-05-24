using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsStateTypeAdapter : BaseAdapter<AudsStateTypeDto, AudsStateType>
    {
        public override AudsStateType Map(AudsStateTypeDto entityDto)
        {
            return entityDto == null ? null : new AudsStateType()
            { 
                StateTypeId = entityDto.StateTypeId, 
                StateTypeName = entityDto.StateTypeName, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsStateTypeDto Map(AudsStateType entity)
        {
            return entity == null ? null : new AudsStateTypeDto()
            { 
                StateTypeId = entity.StateTypeId, 
                StateTypeName = entity.StateTypeName, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsStateType> Map(List<AudsStateTypeDto> listDto)
        {
            List<AudsStateType> list = null;

            if (listDto != null)
            {
                list = new List<AudsStateType>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsStateTypeDto> Map(IList<AudsStateType> list)
        {
            List<AudsStateTypeDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsStateTypeDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}