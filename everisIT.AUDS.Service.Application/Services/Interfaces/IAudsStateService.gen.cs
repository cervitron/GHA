using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsStateService : IGetList<AudsStateDto, IAudsStateFilter>, ICreate<AudsStateDto>, IDelete<AudsStateDto>, IUpdate<AudsStateDto>, IGet<AudsStateDto>
    {
    }
}