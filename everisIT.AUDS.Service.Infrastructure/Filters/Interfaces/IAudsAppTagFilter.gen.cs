using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsAppTag;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsAppTagFilter : IApplicationId, ITagId, ICodeStatus
    {
        IQueryable<Models.AudsAppTag> Filtrate(IQueryable<Models.AudsAppTag> entityModel);
    }
}