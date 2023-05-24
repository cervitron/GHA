using everisIT.AUDS.Service.Infrastructure.Repositories;
using everisIT.AUDS.Service.Infrastructure.Test.DataTest;

namespace everisIT.AUDS.Service.Infrastructure.Test.DataUtil
{
    public partial class AudsAuditHcoRepositoryBaseTest : AudsAuditHcoRepository
    {
        public AudsAuditHcoRepositoryBaseTest(AUDSContextTest aUDSContext) : base(aUDSContext)
        {
        }
    }
}