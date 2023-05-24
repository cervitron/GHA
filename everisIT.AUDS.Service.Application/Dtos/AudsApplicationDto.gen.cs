using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization; 

namespace everisIT.AUDS.Service.Application.Dtos
{
    [ExcludeFromCodeCoverage]
    [DataContract]
    public partial class AudsApplicationDto
    {
        [DataMember(Name = "applicationId")]
        public int ApplicationId { get; set; }
        [DataMember(Name = "applicationName")]
        public string ApplicationName { get; set; }
        [DataMember(Name = "applicationDescription")]
        public string ApplicationDescription { get; set; }
        [DataMember(Name = "groupId")]
        public int GroupId { get; set; }
        [DataMember(Name = "dateLastUpdateRegister")]
        public DateTime DateLastUpdateRegister { get; set; }
        [DataMember(Name = "codeStatus")]
        public bool CodeStatus { get; set; }
    }
}