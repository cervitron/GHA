using System.Collections.Generic;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public partial class AudsApplicationAdapter : BaseAdapter<AudsApplicationDto, AudsApplication>
    {
        public override AudsApplication Map(AudsApplicationDto entityDto)
        {
            return entityDto == null ? null : new AudsApplication()
            { 
                ApplicationId = entityDto.ApplicationId, 
                ApplicationName = entityDto.ApplicationName, 
                ApplicationDescription = entityDto.ApplicationDescription, 
                GroupId = entityDto.GroupId, 
                DateLastUpdateRegister = entityDto.DateLastUpdateRegister, 
                CodeStatus = entityDto.CodeStatus, 
            };
        }

        public override AudsApplicationDto Map(AudsApplication entity)
        {
            return entity == null ? null : new AudsApplicationDto()
            { 
                ApplicationId = entity.ApplicationId, 
                ApplicationName = entity.ApplicationName, 
                ApplicationDescription = entity.ApplicationDescription, 
                GroupId = entity.GroupId, 
                DateLastUpdateRegister = entity.DateLastUpdateRegister, 
                CodeStatus = entity.CodeStatus, 
            };
        }

        public override List<AudsApplication> Map(List<AudsApplicationDto> listDto)
        {
            List<AudsApplication> list = null;

            if (listDto != null)
            {
                list = new List<AudsApplication>();

                foreach (var item in listDto)
                {
                    list.Add(Map(item));
                }
            }
            return list;
        }


        public override IList<AudsApplicationDto> Map(IList<AudsApplication> list)
        {
            List<AudsApplicationDto> listDto = null;

            if (list != null)
            {
                listDto = new List<AudsApplicationDto>();

                foreach (var item in list)
                {
                    listDto.Add(Map(item));
                }
            }
            return listDto;
        }
    }
}