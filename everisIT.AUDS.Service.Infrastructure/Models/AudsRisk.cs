using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsRisk
    {
        public AudsRisk()
        {
            AudsVulnerability = new HashSet<AudsVulnerability>();
            AudsVulnerabilityHco = new HashSet<AudsVulnerabilityHco>();
        }

        public int RiskId { get; set; }
        public string RiskName { get; set; }
        public int HowManyDaysUntilNotification { get; set; }
        public int UserNewRegister { get; set; }
        public DateTime DateNewRegister { get; set; }
        public int UserLastUpdateRegister { get; set; }
        public DateTime DateLastUpdateRegister { get; set; }
        public bool CodeStatus { get; set; }

        public virtual ICollection<AudsVulnerability> AudsVulnerability { get; set; }
        public virtual ICollection<AudsVulnerabilityHco> AudsVulnerabilityHco { get; set; }
    }
}
