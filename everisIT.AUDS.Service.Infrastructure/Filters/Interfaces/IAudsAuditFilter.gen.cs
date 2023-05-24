using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsAudit;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsAuditFilter : IAuditId, IAuditDateStart, IAuditDateEnd, IAuditResolutor, IAuditResponsible, IAuditIsnotificationsent, IApplicationId, IStateId, IIdType, ICodeStatus
    {
        IQueryable<Models.AudsAudit> Filtrate(IQueryable<Models.AudsAudit> entityModel);
    }
}