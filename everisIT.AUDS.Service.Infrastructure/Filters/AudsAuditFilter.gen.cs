using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsAuditFilter : Interfaces.IAudsAuditFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? AuditId { get; set; }
        public System.DateTime? AuditDateStart { get; set; }
        public System.DateTime? AuditDateEnd { get; set; }
        public int? AuditResolutor { get; set; }
        public int? AuditResponsible { get; set; }
        public bool? AuditIsnotificationsent { get; set; }
        public int? ApplicationId { get; set; }
        public int? StateId { get; set; }
        public int? IdType { get; set; }

        public IQueryable<Models.AudsAudit> Filtrate(IQueryable<Models.AudsAudit> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (AuditId != null)
                entityModel = entityModel.Where(model => model.AuditId == AuditId);

            if (AuditDateStart != null)
                entityModel = entityModel.Where(model => model.AuditDateStart == AuditDateStart);

            if (AuditDateEnd != null)
                entityModel = entityModel.Where(model => model.AuditDateEnd == AuditDateEnd);

            if (AuditResolutor != null)
                entityModel = entityModel.Where(model => model.AuditResolutor == AuditResolutor);

            if (AuditResponsible != null)
                entityModel = entityModel.Where(model => model.AuditResponsible == AuditResponsible);

            if (AuditIsnotificationsent != null && AuditIsnotificationsent.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.AuditIsNotificationSent.Equals(AuditIsnotificationsent));

            if (ApplicationId != null)
                entityModel = entityModel.Where(model => model.ApplicationId == ApplicationId);

            if (StateId != null)
                entityModel = entityModel.Where(model => model.StateId == StateId);

            if (IdType != null)
                entityModel = entityModel.Where(model => model.IdType == IdType);

            return entityModel;
        }
    }
}