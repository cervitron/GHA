using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsState;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsStateFilter : IStateId, IStateName, IStateType, ICodeStatus
    {
        IQueryable<Models.AudsState> Filtrate(IQueryable<Models.AudsState> entityModel);
    }
}