namespace everisIT.AUDS.Service.Infrastructure.Interfaces
{
    public partial interface IGetList<EntityModel, IFilter>
    {
        System.Threading.Tasks.Task<System.Collections.Generic.IList<EntityModel>> GetList(IFilter filter);
    }
}
