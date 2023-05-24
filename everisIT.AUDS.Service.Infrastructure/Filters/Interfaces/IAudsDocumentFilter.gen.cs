using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsDocument;
using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces
{
    public partial interface IAudsDocumentFilter : IDocumentId, IDocumentName, IDocumentUserUpload, IDocumentDateUpload, IDocumentDescription, IAuditId, ICodeStatus
    {
        IQueryable<Models.AudsDocument> Filtrate(IQueryable<Models.AudsDocument> entityModel);
    }
}