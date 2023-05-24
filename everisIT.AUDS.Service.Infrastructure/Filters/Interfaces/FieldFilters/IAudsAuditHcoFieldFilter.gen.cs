namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsAuditHco
{
    public interface IAuditHcoId
    {
        int? AuditHcoId { get; set; }
    }

    public interface IAuditId
    {
        int? AuditId { get; set; }
    }

    public interface IAuditDateStart
    {
        System.DateTime? AuditDateStart { get; set; }
    }

    public interface IAuditDateEnd
    {
        System.DateTime? AuditDateEnd { get; set; }
    }

    public interface IAuditResolutor
    {
        int? AuditResolutor { get; set; }
    }

    public interface IAuditResponsible
    {
        int? AuditResponsible { get; set; }
    }

    public interface IAuditIsnotificationsent
    {
        bool? AuditIsnotificationsent { get; set; }
    }

    public interface IApplicationId
    {
        int? ApplicationId { get; set; }
    }

    public interface IStateId
    {
        int? StateId { get; set; }
    }

    public interface IIdType
    {
        int? IdType { get; set; }
    }    
}