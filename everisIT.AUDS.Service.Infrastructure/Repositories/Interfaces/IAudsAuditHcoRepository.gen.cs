using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsAuditHcoRepository : IGetList<AudsAuditHco, IAudsAuditHcoFilter>, ICreate<AudsAuditHco>, IDelete<AudsAuditHco>, IUpdate<AudsAuditHco>, IGet<AudsAuditHco>
    {
    }
}