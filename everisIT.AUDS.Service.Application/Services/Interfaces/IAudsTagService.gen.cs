using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsTagService : IGetList<AudsTagDto, IAudsTagFilter>, ICreate<AudsTagDto>, IDelete<AudsTagDto>, IUpdate<AudsTagDto>, IGet<AudsTagDto>
    {
    }
}