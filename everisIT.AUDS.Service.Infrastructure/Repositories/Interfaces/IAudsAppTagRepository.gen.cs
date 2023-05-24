using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsAppTagRepository : IGetList<AudsAppTag, IAudsAppTagFilter>, ICreate<AudsAppTag>, IDelete<AudsAppTag>, IUpdate<AudsAppTag>, IGet<AudsAppTag>
    {
    }
}