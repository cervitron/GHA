using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AudsStateType
    {
        public AudsStateType()
        {
            AudsState = new HashSet<AudsState>();
        }

        public int StateTypeId { get; set; }
        public string StateTypeName { get; set; }
        public int UserNewRegister { get; set; }
        public DateTime DateNewRegister { get; set; }
        public int UserLastUpdateRegister { get; set; }
        public DateTime DateLastUpdateRegister { get; set; }
        public bool CodeStatus { get; set; }

        public virtual ICollection<AudsState> AudsState { get; set; }
    }
}
