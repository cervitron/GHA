using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsStateAdapter : BaseAdapter<AudsStateDto, AudsState>
    {
        public override AudsState Map(AudsStateDto entityDto)
        {
            return entityDto == null ? null : new AudsState()
            { 
                StateId = entityDto.StateId, 
                StateName = entityDto.StateName, 
                StateType = entityDto.StateType, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsStateDto Map(AudsState entity)
        {
            return entity == null ? null : new AudsStateDto()
            { 
                StateId = entity.StateId, 
                StateName = entity.StateName, 
                StateType = entity.StateType, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsState> Map(List<AudsStateDto> listDto)
        {
            List<AudsState> list = null;

            if (listDto != null)
            {
                list = new List<AudsState>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsStateDto> Map(IList<AudsState> list)
        {
            List<AudsStateDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsStateDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}