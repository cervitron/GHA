using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsStateTypeIntegrationDataTest
    {
        public static AudsStateType ResetEntityDto(AudsStateType entity)
        {
            return new AudsStateType()
            {
                StateTypeId = entity.StateTypeId,
                StateTypeName = entity.StateTypeName,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}