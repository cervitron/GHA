using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsTypeService : IGetList<AudsTypeDto, IAudsTypeFilter>, ICreate<AudsTypeDto>, IDelete<AudsTypeDto>, IUpdate<AudsTypeDto>, IGet<AudsTypeDto>
    {
    }
}