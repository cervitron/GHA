using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization; 

namespace everisIT.AUDS.Service.Application.Dtos
{
    [ExcludeFromCodeCoverage]
    [DataContract]
    public partial class AudsRiskDto
    {
        [DataMember(Name = "riskId")]
        public int RiskId { get; set; }
        [DataMember(Name = "riskName")]
        public string RiskName { get; set; }
        [DataMember(Name = "howManyDaysUntilNotification")]
        public int HowManyDaysUntilNotification { get; set; }
        [DataMember(Name = "dateLastUpdateRegister")]
        public DateTime DateLastUpdateRegister { get; set; }
        [DataMember(Name = "codeStatus")]
        public bool CodeStatus { get; set; }
    }
}