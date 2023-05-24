using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IAudsDocumentService : IGetList<AudsDocumentDto, IAudsDocumentFilter>, ICreate<AudsDocumentDto>, IDelete<AudsDocumentDto>, IUpdate<AudsDocumentDto>, IGet<AudsDocumentDto>
    {
    }
}