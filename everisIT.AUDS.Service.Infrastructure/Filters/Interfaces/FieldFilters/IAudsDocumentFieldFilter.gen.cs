namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsDocument
{
    public interface IDocumentId
    {
        int? DocumentId { get; set; }
    }

    public interface IDocumentName
    {
        string DocumentName { get; set; }
    }

    public interface IDocumentUserUpload
    {
        int? DocumentUserUpload { get; set; }
    }

    public interface IDocumentDateUpload
    {
        System.DateTime? DocumentDateUpload { get; set; }
    }

    public interface IDocumentDescription
    {
        string DocumentDescription { get; set; }
    }

    public interface IAuditId
    {
        int? AuditId { get; set; }
    }    
}