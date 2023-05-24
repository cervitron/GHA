using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsRiskService : IGetList<AudsRiskDto, IAudsRiskFilter>, ICreate<AudsRiskDto>, IDelete<AudsRiskDto>, IUpdate<AudsRiskDto>, IGet<AudsRiskDto>
    {
    }
}