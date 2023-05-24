using everisIT.AUDS.Service.Infrastructure.Repositories;
using everisIT.AUDS.Service.Infrastructure.Test.DataTest;

namespace everisIT.AUDS.Service.Infrastructure.Test.DataUtil
{
    public partial class AudsTagRepositoryBaseTest : AudsTagRepository
    {
        public AudsTagRepositoryBaseTest(AUDSContextTest aUDSContext) : base(aUDSContext)
        {
        }
    }
}