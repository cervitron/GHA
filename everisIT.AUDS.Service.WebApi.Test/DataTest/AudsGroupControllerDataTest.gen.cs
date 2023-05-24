using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsGroupControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsGroupDto> GetAudsGroupDtoList()
        {
            return new System.Collections.Generic.List<AudsGroupDto>()
            {
                new AudsGroupDto()
                {
                    GroupId = 3,
                    GroupName = "AdminPutTest",
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsGroupDto()
                {
                    GroupId = 4,
                    GroupName = "AdminPutTest",
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsGroupDto GetAudsGroupDto()
        {
            return GetAudsGroupDtoList().FirstOrDefault();
        }
    }
}