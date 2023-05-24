namespace everisIT.AUDS.Service.Infrastructure.Interfaces
{
    public partial interface ICreate<EntityModel>
    {
        System.Threading.Tasks.Task<EntityModel> Create(EntityModel dataModel);
    }
}
