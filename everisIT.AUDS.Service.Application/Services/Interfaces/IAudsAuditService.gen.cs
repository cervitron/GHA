using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsAuditService : IGetList<AudsAuditDto, IAudsAuditFilter>, ICreate<AudsAuditDto>, IDelete<AudsAuditDto>, IUpdate<AudsAuditDto>, IGet<AudsAuditDto>
    {
    }
}