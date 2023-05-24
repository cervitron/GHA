using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsAuditRepository : IGetList<AudsAudit, IAudsAuditFilter>, ICreate<AudsAudit>, IDelete<AudsAudit>, IUpdate<AudsAudit>, IGet<AudsAudit>
    {
    }
}