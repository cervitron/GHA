namespace everisIT.AUDS.Service.Infrastructure.Filters.Interfaces.FieldFilters
{
    public interface ICodeStatusSam
    {
        /// <summary>
        /// Filter the status (Null=All,"A"=OnlyActive,"B"=OnlyInactive)
        /// </summary>
        string CodeStatus { get; set; }
    }
}
