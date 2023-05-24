using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsRisk;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsRiskFilter : IRiskId, IRiskName, IHowManyDaysUntilNotification, ICodeStatus
    {
        IQueryable<Models.AudsRisk> Filtrate(IQueryable<Models.AudsRisk> entityModel);
    }
}