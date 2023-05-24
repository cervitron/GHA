using System.Linq;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsAuditResponsibleFilter : ICodeStatus
    {
        IQueryable<Models.AudsAuditResponsible> Filtrate(IQueryable<Models.AudsAuditResponsible> entityModel);
    }
}