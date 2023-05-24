using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsApplicationFilter : Interfaces.IAudsApplicationFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int? GroupId { get; set; }

        public IQueryable<Models.AudsApplication> Filtrate(IQueryable<Models.AudsApplication> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (ApplicationId != null)
                entityModel = entityModel.Where(model => model.ApplicationId == ApplicationId);

            if(!string.IsNullOrEmpty(ApplicationName))
                entityModel = entityModel.Where(model => model.ApplicationName.ToLower().Contains(ApplicationName.ToLower()));

            if (GroupId != null)
                entityModel = entityModel.Where(model => model.GroupId == GroupId);

            return entityModel;
        }
    }
}