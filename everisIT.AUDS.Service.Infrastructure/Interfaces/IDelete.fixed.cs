namespace everisIT.AUDS.Service.Infrastructure.Interfaces
{
    public partial interface IDelete<EntityModel>
    {
        System.Threading.Tasks.Task<EntityModel> Delete(int id);
    }
}
