using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsAuditHco;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsAuditHcoFilter : IAuditHcoId, IAuditId, IAuditDateStart, IAuditDateEnd, IAuditResolutor, IAuditResponsible, IAuditIsnotificationsent, IApplicationId, IStateId, IIdType, ICodeStatus
    {
        IQueryable<Models.AudsAuditHco> Filtrate(IQueryable<Models.AudsAuditHco> entityModel);
    }
}