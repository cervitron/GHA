using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsTag;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsTagFilter : ITagId, ITagName, ICodeStatus
    {
        IQueryable<Models.AudsTag> Filtrate(IQueryable<Models.AudsTag> entityModel);
    }
}