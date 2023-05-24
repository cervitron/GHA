using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsStateTypeRepository : IGetList<AudsStateType, IAudsStateTypeFilter>, ICreate<AudsStateType>, IDelete<AudsStateType>, IUpdate<AudsStateType>, IGet<AudsStateType>
    {
    }
}