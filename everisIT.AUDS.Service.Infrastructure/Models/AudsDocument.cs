using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsDocument
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public int DocumentUserUpload { get; set; }
        public DateTime DocumentDateUpload { get; set; }
        public string DocumentDescription { get; set; }
        public int AuditId { get; set; }
        public int UserNewRegister { get; set; }
        public DateTime DateNewRegister { get; set; }
        public int UserLastUpdateRegister { get; set; }
        public DateTime DateLastUpdateRegister { get; set; }
        public bool CodeStatus { get; set; }

        public virtual AudsAudit Audit { get; set; }
    }
}
