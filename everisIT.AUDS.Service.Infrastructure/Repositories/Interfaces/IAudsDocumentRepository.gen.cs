using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsDocumentRepository : IGetList<AudsDocument, IAudsDocumentFilter>, ICreate<AudsDocument>, IDelete<AudsDocument>, IUpdate<AudsDocument>, IGet<AudsDocument>
    {
    }
}