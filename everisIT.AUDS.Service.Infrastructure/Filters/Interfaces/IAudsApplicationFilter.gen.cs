using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsApplication;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsApplicationFilter : IApplicationId, IApplicationName, IGroupId, ICodeStatus
    {
        IQueryable<Models.AudsApplication> Filtrate(IQueryable<Models.AudsApplication> entityModel);
    }
}