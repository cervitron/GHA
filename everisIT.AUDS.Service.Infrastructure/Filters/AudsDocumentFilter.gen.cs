using System.Linq;

namespace everisIT.AUDS.Service.Infrastructure.Filters
{
    public partial class AudsDocumentFilter : Interfaces.IAudsDocumentFilter
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        public bool? CodeStatus { get; set; }
        public int? DocumentId { get; set; }
        public string DocumentName { get; set; }
        public int? DocumentUserUpload { get; set; }
        public System.DateTime? DocumentDateUpload { get; set; }
        public string DocumentDescription { get; set; }
        public int? AuditId { get; set; }

        public IQueryable<Models.AudsDocument> Filtrate(IQueryable<Models.AudsDocument> entityModel)
        {
            if (CodeStatus != null && CodeStatus.HasValue)
                entityModel = entityModel.Where(entityModel => entityModel.CodeStatus.Equals(CodeStatus));

            if (DocumentId != null)
                entityModel = entityModel.Where(model => model.DocumentId == DocumentId);

            if(!string.IsNullOrEmpty(DocumentName))
                entityModel = entityModel.Where(model => model.DocumentName.ToLower().Contains(DocumentName.ToLower()));

            if (DocumentUserUpload != null)
                entityModel = entityModel.Where(model => model.DocumentUserUpload == DocumentUserUpload);

            if (DocumentDateUpload != null)
                entityModel = entityModel.Where(model => model.DocumentDateUpload == DocumentDateUpload);

            if(!string.IsNullOrEmpty(DocumentDescription))
                entityModel = entityModel.Where(model => model.DocumentDescription.ToLower().Contains(DocumentDescription.ToLower()));

            if (AuditId != null)
                entityModel = entityModel.Where(model => model.AuditId == AuditId);

            return entityModel;
        }
    }
}