using everisIT.Fen2.Utils.EF;
using Microsoft.EntityFrameworkCore;

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    /// <summary>
    /// Execute EntityFrameWork Scaffolding for existing DB 
    /// https://docs.microsoft.com/es-es/ef/core/get-started/aspnetcore/existing-db
    /// </summary>
    public partial class AUDSContext : DbContexWithAudit
    {
        public AUDSContext(IUserResolverService userResolver)
            : base(userResolver)
        {
        }
        public AUDSContext(IUserResolverService userResolver, DbContextOptions<AUDSContext> options)
            : base(userResolver, options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
