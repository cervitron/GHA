using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsApplicationIntegrationDataTest
    {
        public static AudsApplication ResetEntityDto(AudsApplication entity)
        {
            return new AudsApplication()
            {
                ApplicationId = entity.ApplicationId,
                ApplicationName = entity.ApplicationName,
                ApplicationDescription = entity.ApplicationDescription,
                GroupId = entity.GroupId,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}