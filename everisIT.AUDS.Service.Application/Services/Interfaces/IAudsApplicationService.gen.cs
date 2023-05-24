using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsApplicationService : IGetList<AudsApplicationDto, IAudsApplicationFilter>, ICreate<AudsApplicationDto>, IDelete<AudsApplicationDto>, IUpdate<AudsApplicationDto>, IGet<AudsApplicationDto>
    {
    }
}