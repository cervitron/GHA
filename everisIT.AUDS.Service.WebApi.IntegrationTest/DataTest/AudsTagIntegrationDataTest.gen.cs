using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsTagIntegrationDataTest
    {
        public static AudsTag ResetEntityDto(AudsTag entity)
        {
            return new AudsTag()
            {
                TagId = entity.TagId,
                TagName = entity.TagName,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}