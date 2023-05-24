using everisIT.AUDS.Service.Infrastructure.Repositories;
using everisIT.AUDS.Service.Infrastructure.Test.DataTest;

namespace everisIT.AUDS.Service.Infrastructure.Test.DataUtil
{
    public partial class AudsDocumentRepositoryBaseTest : AudsDocumentRepository
    {
        public AudsDocumentRepositoryBaseTest(AUDSContextTest aUDSContext) : base(aUDSContext)
        {
        }
    }
}