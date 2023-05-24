using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsAppTagService : IGetList<AudsAppTagDto, IAudsAppTagFilter>, ICreate<AudsAppTagDto>, IDelete<AudsAppTagDto>, IUpdate<AudsAppTagDto>, IGet<AudsAppTagDto>
    {
    }
}