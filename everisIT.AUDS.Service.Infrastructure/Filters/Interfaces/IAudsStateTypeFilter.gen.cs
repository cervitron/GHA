using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsStateType;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsStateTypeFilter : IStateTypeId, IStateTypeName, ICodeStatus
    {
        IQueryable<Models.AudsStateType> Filtrate(IQueryable<Models.AudsStateType> entityModel);
    }
}