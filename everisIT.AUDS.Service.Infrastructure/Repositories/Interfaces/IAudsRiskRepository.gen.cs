using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsRiskRepository : IGetList<AudsRisk, IAudsRiskFilter>, ICreate<AudsRisk>, IDelete<AudsRisk>, IUpdate<AudsRisk>, IGet<AudsRisk>
    {
    }
}