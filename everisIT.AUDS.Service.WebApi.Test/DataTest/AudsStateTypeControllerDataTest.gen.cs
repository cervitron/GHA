using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsStateTypeControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsStateTypeDto> GetAudsStateTypeDtoList()
        {
            return new System.Collections.Generic.List<AudsStateTypeDto>()
            {
                new AudsStateTypeDto()
                {
                    StateTypeId = 3,
                    StateTypeName = "AdminPutTest",
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsStateTypeDto()
                {
                    StateTypeId = 4,
                    StateTypeName = "AdminPutTest",
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsStateTypeDto GetAudsStateTypeDto()
        {
            return GetAudsStateTypeDtoList().FirstOrDefault();
        }
    }
}