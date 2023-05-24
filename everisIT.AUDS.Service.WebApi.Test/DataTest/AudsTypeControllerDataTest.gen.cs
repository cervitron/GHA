using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsTypeControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsTypeDto> GetAudsTypeDtoList()
        {
            return new System.Collections.Generic.List<AudsTypeDto>()
            {
                new AudsTypeDto()
                {
                    IdType = 3,
                    NameType = "AdminPutTest",
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsTypeDto()
                {
                    IdType = 4,
                    NameType = "AdminPutTest",
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsTypeDto GetAudsTypeDto()
        {
            return GetAudsTypeDtoList().FirstOrDefault();
        }
    }
}