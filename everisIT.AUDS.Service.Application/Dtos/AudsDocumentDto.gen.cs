using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization; 

namespace everisIT.AUDS.Service.Application.Dtos
{
    [ExcludeFromCodeCoverage]
    [DataContract]
    public partial class AudsDocumentDto
    {
        [DataMember(Name = "documentId")]
        public int DocumentId { get; set; }
        [DataMember(Name = "documentName")]
        public string DocumentName { get; set; }
        [DataMember(Name = "documentUserUpload")]
        public int DocumentUserUpload { get; set; }
        [DataMember(Name = "documentDateUpload")]
        public DateTime DocumentDateUpload { get; set; }
        [DataMember(Name = "documentDescription")]
        public string DocumentDescription { get; set; }
        [DataMember(Name = "auditId")]
        public int AuditId { get; set; }
        [DataMember(Name = "dateLastUpdateRegister")]
        public DateTime DateLastUpdateRegister { get; set; }
        [DataMember(Name = "codeStatus")]
        public bool CodeStatus { get; set; }
    }
}