namespace everisIT.AUDS.Service.Infrastructure.Interfaces
{
    public partial interface IUpdate<EntityModel>
    {
        System.Threading.Tasks.Task<EntityModel> Update(EntityModel dataModel);
    }
}
