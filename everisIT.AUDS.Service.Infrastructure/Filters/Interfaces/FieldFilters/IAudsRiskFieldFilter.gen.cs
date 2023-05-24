namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsRisk
{
    public interface IRiskId
    {
        int? RiskId { get; set; }
    }

    public interface IRiskName
    {
        string RiskName { get; set; }
    }

    public interface IHowManyDaysUntilNotification
    {
        int? HowManyDaysUntilNotification { get; set; }
    }    
}