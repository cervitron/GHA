using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsAppTagControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsAppTagDto> GetAudsAppTagDtoList()
        {
            return new System.Collections.Generic.List<AudsAppTagDto>()
            {
                new AudsAppTagDto()
                {
                    ApplicationId = 3,
                    TagId = 3,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsAppTagDto()
                {
                    ApplicationId = 4,
                    TagId = 4,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsAppTagDto GetAudsAppTagDto()
        {
            return GetAudsAppTagDtoList().FirstOrDefault();
        }
    }
}