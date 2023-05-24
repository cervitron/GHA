using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsAppTagFilter : Interfaces.IAudsAppTagFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? ApplicationId { get; set; }
        public int? TagId { get; set; }

        public IQueryable<Models.AudsAppTag> Filtrate(IQueryable<Models.AudsAppTag> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (ApplicationId != null)
                entityModel = entityModel.Where(model => model.ApplicationId == ApplicationId);

            if (TagId != null)
                entityModel = entityModel.Where(model => model.TagId == TagId);

            return entityModel;
        }
    }
}