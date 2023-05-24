using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace everisIT.AUDS.Service.Application.Dtos
{

    [ExcludeFromCodeCoverage]
    [DataContract]
    public partial class AudsAuditResponsibleDto : Fen2.Utils.EF.DbModelAuditFen2
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "auditId")]
        public int AuditId { get; set; }
        [DataMember(Name = "idEmployee")]
        public int IdEmployee { get; set; }
    }
}