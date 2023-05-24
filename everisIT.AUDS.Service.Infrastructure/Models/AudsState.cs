using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsState
    {
        public AudsState()
        {
            AudsAudit = new HashSet<AudsAudit>();
            AudsAuditHco = new HashSet<AudsAuditHco>();
            AudsVulnerabilityHcoState = new HashSet<AudsVulnerabilityHco>();
            AudsVulnerabilityHcoStateIdResolutionNavigation = new HashSet<AudsVulnerabilityHco>();
            AudsVulnerabilityState = new HashSet<AudsVulnerability>();
            AudsVulnerabilityStateIdResolutionNavigation = new HashSet<AudsVulnerability>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public int StateType { get; set; }
        public int UserNewRegister { get; set; }
        public DateTime DateNewRegister { get; set; }
        public int UserLastUpdateRegister { get; set; }
        public DateTime DateLastUpdateRegister { get; set; }
        public bool CodeStatus { get; set; }

        public virtual AudsStateType StateTypeNavigation { get; set; }
        public virtual ICollection<AudsAudit> AudsAudit { get; set; }
        public virtual ICollection<AudsAuditHco> AudsAuditHco { get; set; }
        public virtual ICollection<AudsVulnerabilityHco> AudsVulnerabilityHcoState { get; set; }
        public virtual ICollection<AudsVulnerabilityHco> AudsVulnerabilityHcoStateIdResolutionNavigation { get; set; }
        public virtual ICollection<AudsVulnerability> AudsVulnerabilityState { get; set; }
        public virtual ICollection<AudsVulnerability> AudsVulnerabilityStateIdResolutionNavigation { get; set; }
    }
}
