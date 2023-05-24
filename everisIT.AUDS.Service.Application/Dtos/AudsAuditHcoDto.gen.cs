using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization; 

namespace everisIT.AUDS.Service.Application.Dtos
{
    [ExcludeFromCodeCoverage]
    [DataContract]
    public partial class AudsAuditHcoDto
    {
        [DataMember(Name = "auditHcoId")]
        public int AuditHcoId { get; set; }
        [DataMember(Name = "auditId")]
        public int AuditId { get; set; }
        [DataMember(Name = "auditDateStart")]
        public DateTime AuditDateStart { get; set; }
        [DataMember(Name = "auditDateEnd")]
        public DateTime AuditDateEnd { get; set; }
        [DataMember(Name = "auditResolutor")]
        public int AuditResolutor { get; set; }
        [DataMember(Name = "auditResponsible")]
        public int AuditResponsible { get; set; }
        [DataMember(Name = "auditDescription")]
        public string AuditDescription { get; set; }
        [DataMember(Name = "auditIsnotificationsent")]
        public bool AuditIsnotificationsent { get; set; }
        [DataMember(Name = "applicationId")]
        public int ApplicationId { get; set; }
        [DataMember(Name = "stateId")]
        public int StateId { get; set; }
        [DataMember(Name = "idType")]
        public int IdType { get; set; }
        [DataMember(Name = "dateLastUpdateRegister")]
        public DateTime DateLastUpdateRegister { get; set; }
        [DataMember(Name = "codeStatus")]
        public bool CodeStatus { get; set; }
    }
}