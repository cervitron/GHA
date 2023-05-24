using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsAppTagIntegrationDataTest
    {
        public static AudsAppTag ResetEntityDto(AudsAppTag entity)
        {
            return new AudsAppTag()
            {
                ApplicationId = entity.ApplicationId,
                TagId = entity.TagId,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}