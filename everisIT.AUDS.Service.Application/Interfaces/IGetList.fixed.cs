namespace everisIT.AUDS.Service.Application.Interfaces
{
    public partial interface IGetList<EntityDto, Filter>
    {
        System.Threading.Tasks.Task<System.Collections.Generic.IList<EntityDto>> GetList(Filter filter);
    }
}
