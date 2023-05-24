using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsStateIntegrationDataTest
    {
        public static AudsState ResetEntityDto(AudsState entity)
        {
            return new AudsState()
            {
                StateId = entity.StateId,
                StateName = entity.StateName,
                StateType = entity.StateType,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}