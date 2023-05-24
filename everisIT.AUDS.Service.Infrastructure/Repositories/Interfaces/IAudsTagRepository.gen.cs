using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsTagRepository : IGetList<AudsTag, IAudsTagFilter>, ICreate<AudsTag>, IDelete<AudsTag>, IUpdate<AudsTag>, IGet<AudsTag>
    {
    }
}