using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;

namespace everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces
{
    public partial interface IAudsApplicationRepository : IGetList<AudsApplication, IAudsApplicationFilter>, ICreate<AudsApplication>, IDelete<AudsApplication>, IUpdate<AudsApplication>, IGet<AudsApplication>
    {
    }
}