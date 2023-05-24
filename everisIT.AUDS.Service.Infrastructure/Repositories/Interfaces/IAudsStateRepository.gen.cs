using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsStateRepository : IGetList<AudsState, IAudsStateFilter>, ICreate<AudsState>, IDelete<AudsState>, IUpdate<AudsState>, IGet<AudsState>
    {
    }
}