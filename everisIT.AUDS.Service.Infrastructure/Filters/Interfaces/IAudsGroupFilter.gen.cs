using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsGroup;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsGroupFilter : IGroupId, IGroupName, ICodeStatus
    {
        IQueryable<Models.AudsGroup> Filtrate(IQueryable<Models.AudsGroup> entityModel);
    }
}