using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsApplicationControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsApplicationDto> GetAudsApplicationDtoList()
        {
            return new System.Collections.Generic.List<AudsApplicationDto>()
            {
                new AudsApplicationDto()
                {
                    ApplicationId = 3,
                    ApplicationName = "AdminPutTest",
                    ApplicationDescription = "AdminPutTest",
                    GroupId = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsApplicationDto()
                {
                    ApplicationId = 4,
                    ApplicationName = "AdminPutTest",
                    ApplicationDescription = "AdminPutTest",
                    GroupId = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsApplicationDto GetAudsApplicationDto()
        {
            return GetAudsApplicationDtoList().FirstOrDefault();
        }
    }
}