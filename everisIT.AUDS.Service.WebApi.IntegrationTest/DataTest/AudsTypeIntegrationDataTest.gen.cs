using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest
{
    public partial class AudsTypeIntegrationDataTest
    {
        public static AudsType ResetEntityDto(AudsType entity)
        {
            return new AudsType()
            {
                IdType = entity.IdType,
                NameType = entity.NameType,
                DateLastUpdateRegister = entity.DateLastUpdateRegister,
                CodeStatus = entity.CodeStatus
            };
        }
    }
}