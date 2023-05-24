using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsAuditResponsible : Fen2.Utils.EF.DbModelAuditFen2
    {
        public int Id { get; set; }
        public int AuditId { get; set; }
        public int IdEmployee { get; set; }

        public virtual AudsAudit Audit { get; set; }
    }
}
