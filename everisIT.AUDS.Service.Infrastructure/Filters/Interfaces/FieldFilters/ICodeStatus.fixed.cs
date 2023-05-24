namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters
{
    public partial interface ICodeStatus
    {
        /// <summary>
        /// Filter the status (Null=All,True=OnlyActive,False=OnlyInactive)
        /// </summary>
        bool? CodeStatus { get; set; }
    }
}
