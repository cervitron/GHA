using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsStateTypeFilter : Interfaces.IAudsStateTypeFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? StateTypeId { get; set; }
        public string StateTypeName { get; set; }

        public IQueryable<Models.AudsStateType> Filtrate(IQueryable<Models.AudsStateType> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (StateTypeId != null)
                entityModel = entityModel.Where(model => model.StateTypeId == StateTypeId);

            if(!string.IsNullOrEmpty(StateTypeName))
                entityModel = entityModel.Where(model => model.StateTypeName.ToLower().Contains(StateTypeName.ToLower()));

            return entityModel;
        }
    }
}