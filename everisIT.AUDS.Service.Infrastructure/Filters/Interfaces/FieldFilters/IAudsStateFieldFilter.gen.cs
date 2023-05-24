namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters.AudsState
{
    public interface IStateId
    {
        int? StateId { get; set; }
    }

    public interface IStateName
    {
        string StateName { get; set; }
    }

    public interface IStateType
    {
        int? StateType { get; set; }
    }    
}