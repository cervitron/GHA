using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsStateTypeService : IGetList<AudsStateTypeDto, IAudsStateTypeFilter>, ICreate<AudsStateTypeDto>, IDelete<AudsStateTypeDto>, IUpdate<AudsStateTypeDto>, IGet<AudsStateTypeDto>
    {
    }
}