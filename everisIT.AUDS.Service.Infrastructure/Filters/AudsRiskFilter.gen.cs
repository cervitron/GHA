using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsRiskFilter : Interfaces.IAudsRiskFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? RiskId { get; set; }
        public string RiskName { get; set; }
        public int? HowManyDaysUntilNotification { get; set; }

        public IQueryable<Models.AudsRisk> Filtrate(IQueryable<Models.AudsRisk> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (RiskId != null)
                entityModel = entityModel.Where(model => model.RiskId == RiskId);

            if(!string.IsNullOrEmpty(RiskName))
                entityModel = entityModel.Where(model => model.RiskName.ToLower().Contains(RiskName.ToLower()));

            if (HowManyDaysUntilNotification != null)
                entityModel = entityModel.Where(model => model.HowManyDaysUntilNotification == HowManyDaysUntilNotification);

            return entityModel;
        }
    }
}