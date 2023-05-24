namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsApplication
{
    public interface IApplicationId
    {
        int? ApplicationId { get; set; }
    }

    public interface IApplicationName
    {
        string ApplicationName { get; set; }
    }

    public interface IGroupId
    {
        int? GroupId { get; set; }
    }    
}