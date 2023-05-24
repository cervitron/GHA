using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsAudit : Fen2.Utils.EF.DbModelAuditFen2
    {
        public AudsAudit()
        {
            AudsAuditHco = new HashSet<AudsAuditHco>();
            AudsAuditResponsible = new HashSet<AudsAuditResponsible>();
            AudsDocument = new HashSet<AudsDocument>();
            AudsVulnerability = new HashSet<AudsVulnerability>();
            AudsVulnerabilityHco = new HashSet<AudsVulnerabilityHco>();
        }

        public int AuditId { get; set; }
        public DateTime AuditDateStart { get; set; }
        public DateTime AuditDateEnd { get; set; }
        public int AuditResolutor { get; set; }
        public int AuditResponsible { get; set; }
        public string AuditDescription { get; set; }
        public bool AuditIsNotificationSent { get; set; }
        public bool AuditIsNotificationEndSent { get; set; }
        public int ApplicationId { get; set; }
        public int StateId { get; set; }
        public int IdType { get; set; }

        public virtual AudsApplication Application { get; set; }
        public virtual AudsType IdTypeNavigation { get; set; }
        public virtual AudsState State { get; set; }
        public virtual ICollection<AudsAuditHco> AudsAuditHco { get; set; }
        public virtual ICollection<AudsAuditResponsible> AudsAuditResponsible { get; set; }
        public virtual ICollection<AudsDocument> AudsDocument { get; set; }
        public virtual ICollection<AudsVulnerability> AudsVulnerability { get; set; }
        public virtual ICollection<AudsVulnerabilityHco> AudsVulnerabilityHco { get; set; }
    }
}
