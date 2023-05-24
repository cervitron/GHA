using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsApplication
    {
        public AudsApplication()
        {
            AudsAppTag = new HashSet<AudsAppTag>();
            AudsAudit = new HashSet<AudsAudit>();
            AudsAuditHco = new HashSet<AudsAuditHco>();
        }

        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
        public int GroupId { get; set; }
        public int UserNewRegister { get; set; }
        public DateTime DateNewRegister { get; set; }
        public int UserLastUpdateRegister { get; set; }
        public DateTime DateLastUpdateRegister { get; set; }
        public bool CodeStatus { get; set; }

        public virtual AudsGroup Group { get; set; }
        public virtual ICollection<AudsAppTag> AudsAppTag { get; set; }
        public virtual ICollection<AudsAudit> AudsAudit { get; set; }
        public virtual ICollection<AudsAuditHco> AudsAuditHco { get; set; }
    }
}
