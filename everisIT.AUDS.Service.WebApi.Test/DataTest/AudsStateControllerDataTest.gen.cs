using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Linq;

namespace everisIT.AUDS.Service.WebApi.Test.DataTest
{
    public partial class AudsStateControllerDataTest
    {        
        public static System.Collections.Generic.IList<AudsStateDto> GetAudsStateDtoList()
        {
            return new System.Collections.Generic.List<AudsStateDto>()
            {
                new AudsStateDto()
                {
                    StateId = 3,
                    StateName = "AdminPutTest",
                    StateType = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                },
                new AudsStateDto()
                {
                    StateId = 4,
                    StateName = "AdminPutTest",
                    StateType = 100,
                    DateLastUpdateRegister = DateTime.Now,
                    CodeStatus = true
                }
            };
        }

        public static AudsStateDto GetAudsStateDto()
        {
            return GetAudsStateDtoList().FirstOrDefault();
        }
    }
}