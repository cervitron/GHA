using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsGroupService : IGetList<AudsGroupDto, IAudsGroupFilter>, ICreate<AudsGroupDto>, IDelete<AudsGroupDto>, IUpdate<AudsGroupDto>, IGet<AudsGroupDto>
    {
    }
}