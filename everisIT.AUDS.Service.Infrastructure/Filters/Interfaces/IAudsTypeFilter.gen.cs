using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsType;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsTypeFilter : IIdType, INameType, ICodeStatus
    {
        IQueryable<Models.AudsType> Filtrate(IQueryable<Models.AudsType> entityModel);
    }
}