using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization; 

namespace everisIT.AUDS.Service.Application.Dtos
{
    [ExcludeFromCodeCoverage]
    [DataContract]
    public partial class AudsStateDto
    {
        [DataMember(Name = "stateId")]
        public int StateId { get; set; }
        [DataMember(Name = "stateName")]
        public string StateName { get; set; }
        [DataMember(Name = "stateType")]
        public int StateType { get; set; }
        [DataMember(Name = "dateLastUpdateRegister")]
        public DateTime DateLastUpdateRegister { get; set; }
        [DataMember(Name = "codeStatus")]
        public bool CodeStatus { get; set; }
    }
}