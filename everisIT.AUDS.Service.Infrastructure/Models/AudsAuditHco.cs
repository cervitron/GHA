using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsAuditHco
    {
        public int AuditHcoId { get; set; }
        public int AuditId { get; set; }
        public DateTime AuditDateStart { get; set; }
        public DateTime AuditDateEnd { get; set; }
        public int AuditResolutor { get; set; }
        public int AuditResponsible { get; set; }
        public string AuditDescription { get; set; }
        public bool AuditIsNotificationSent { get; set; }
        public int ApplicationId { get; set; }
        public int StateId { get; set; }
        public int IdType { get; set; }
        public int UserNewRegister { get; set; }
        public DateTime DateNewRegister { get; set; }
        public int UserLastUpdateRegister { get; set; }
        public DateTime DateLastUpdateRegister { get; set; }
        public bool CodeStatus { get; set; }

        public virtual AudsApplication Application { get; set; }
        public virtual AudsAudit Audit { get; set; }
        public virtual AudsType IdTypeNavigation { get; set; }
        public virtual AudsState State { get; set; }
    }
}
