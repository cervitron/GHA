using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsTagControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsTagDto> GetAudsTagDtoList()
        {
            return new System.Collections.Generic.List<AudsTagDto>()
            {
                new AudsTagDto()
                {
                    TagId = 3,
                    TagName = "AdminPutTest",
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsTagDto()
                {
                    TagId = 4,
                    TagName = "AdminPutTest",
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsTagDto GetAudsTagDto()
        {
            return GetAudsTagDtoList().FirstOrDefault();
        }
    }
}