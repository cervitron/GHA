using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsTypeRepository : IGetList<AudsType, IAudsTypeFilter>, ICreate<AudsType>, IDelete<AudsType>, IUpdate<AudsType>, IGet<AudsType>
    {
    }
}