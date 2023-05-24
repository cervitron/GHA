using everisIT.AUDS.Service.Infrastructure.Models;
using System;

namespace everisIT.AUDS.Service.Infrastructure.Test.DataTest
{
    public partial class AudsApplicationRepositoryDataTest
    {
        public static void LoadContext(AUDSContextTest aUDSContextTest)
        {

            var model1 = new AudsApplication()
            {
                ApplicationId = 3,
                CodeStatus = true,
                UserNewRegister = -1,
                DateNewRegister = new DateTime(),
                UserLastUpdateRegister = -1,
                DateLastUpdateRegister = new DateTime()
            };

            aUDSContextTest.Add(model1);

            var model2 = new AudsApplication()
            {
                ApplicationId = 4,
                CodeStatus = true,
                UserNewRegister = -1,
                DateNewRegister = new DateTime(),
                UserLastUpdateRegister = -1,
                DateLastUpdateRegister = new DateTime()
            };

            aUDSContextTest.Add(model2);

            aUDSContextTest.SaveChanges();
        }
    }
}