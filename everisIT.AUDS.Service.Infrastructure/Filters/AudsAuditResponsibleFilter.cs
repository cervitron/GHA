using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsAuditResponsibleFilter : Interfaces.IAudsAuditResponsibleFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? Id { get; set; }
        public int? AuditId { get; set; }
        public int? IdEmployee { get; set; }

        public IQueryable<Models.AudsAuditResponsible> Filtrate(IQueryable<Models.AudsAuditResponsible> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (Id != null)
                entityModel = entityModel.Where(model => model.Id == Id);

            if (AuditId != null)
                entityModel = entityModel.Where(model => model.AuditId == AuditId);

            if (IdEmployee != null)
                entityModel = entityModel.Where(model => model.IdEmployee == IdEmployee);

            return entityModel;
        }
    }
}