using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsAuditHcoService : IGetList<AudsAuditHcoDto, IAudsAuditHcoFilter>, ICreate<AudsAuditHcoDto>, IDelete<AudsAuditHcoDto>, IUpdate<AudsAuditHcoDto>, IGet<AudsAuditHcoDto>
    {
    }
}