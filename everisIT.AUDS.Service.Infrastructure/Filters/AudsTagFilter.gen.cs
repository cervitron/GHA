using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsTagFilter : Interfaces.IAudsTagFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? TagId { get; set; }
        public string TagName { get; set; }

        public IQueryable<Models.AudsTag> Filtrate(IQueryable<Models.AudsTag> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (TagId != null)
                entityModel = entityModel.Where(model => model.TagId == TagId);

            if(!string.IsNullOrEmpty(TagName))
                entityModel = entityModel.Where(model => model.TagName.ToLower().Contains(TagName.ToLower()));

            return entityModel;
        }
    }
}