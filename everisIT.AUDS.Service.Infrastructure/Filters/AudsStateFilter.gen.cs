using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsStateFilter : Interfaces.IAudsStateFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? StateType { get; set; }

        public IQueryable<Models.AudsState> Filtrate(IQueryable<Models.AudsState> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (StateId != null)
                entityModel = entityModel.Where(model => model.StateId == StateId);

            if(!string.IsNullOrEmpty(StateName))
                entityModel = entityModel.Where(model => model.StateName.ToLower().Contains(StateName.ToLower()));

            if (StateType != null)
                entityModel = entityModel.Where(model => model.StateType == StateType);

            return entityModel;
        }
    }
}