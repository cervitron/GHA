using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsGroupIntegrationDataTest
    {
        public static AudsGroup ResetEntityDto(AudsGroup entity)
        {
            return new AudsGroup()
            {
                GroupId = entity.GroupId,
                GroupName = entity.GroupName,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}