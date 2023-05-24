using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsGroupFilter : Interfaces.IAudsGroupFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }

        public IQueryable<Models.AudsGroup> Filtrate(IQueryable<Models.AudsGroup> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (GroupId != null)
                entityModel = entityModel.Where(model => model.GroupId == GroupId);

            if(!string.IsNullOrEmpty(GroupName))
                entityModel = entityModel.Where(model => model.GroupName.ToLower().Contains(GroupName.ToLower()));

            return entityModel;
        }
    }
}