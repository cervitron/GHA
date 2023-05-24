using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsTypeFilter : Interfaces.IAudsTypeFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? IdType { get; set; }
        public string NameType { get; set; }

        public IQueryable<Models.AudsType> Filtrate(IQueryable<Models.AudsType> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (IdType != null)
                entityModel = entityModel.Where(model => model.IdType == IdType);

            if(!string.IsNullOrEmpty(NameType))
                entityModel = entityModel.Where(model => model.NameType.ToLower().Contains(NameType.ToLower()));

            return entityModel;
        }
    }
}