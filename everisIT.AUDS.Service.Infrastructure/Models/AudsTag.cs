using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsTag
    {
        public AudsTag()
        {
            AudsAppTag = new HashSet<AudsAppTag>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; }
        public int UserNewRegister { get; set; }
        public DateTime DateNewRegister { get; set; }
        public int UserLastUpdateRegister { get; set; }
        public DateTime DateLastUpdateRegister { get; set; }
        public bool CodeStatus { get; set; }

        public virtual ICollection<AudsAppTag> AudsAppTag { get; set; }
    }
}
