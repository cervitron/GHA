using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsGroupRepository : IGetList<AudsGroup, IAudsGroupFilter>, ICreate<AudsGroup>, IDelete<AudsGroup>, IUpdate<AudsGroup>, IGet<AudsGroup>
    {
    }
}