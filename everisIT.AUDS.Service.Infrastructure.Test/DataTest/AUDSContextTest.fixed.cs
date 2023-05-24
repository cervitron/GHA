using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.Fen2.Utils.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace everisIT.AUDS.Service.Infrastructure.Test.DataTest
{
    public class AUDSContextTest : AUDSContext
    {
        public AUDSContextTest(IUserResolverService userResolver, DbContextOptions<AUDSContext> options) : base(userResolver, options){ }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
                base.OnConfiguring(optionsBuilder
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

    }
}
