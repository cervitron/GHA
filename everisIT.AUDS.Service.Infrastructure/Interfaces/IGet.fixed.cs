namespace everisIT.AUDS.Service.Infrastructure.Interfaces
{
    public partial interface IGet<EntityModel>
    {
        System.Threading.Tasks.Task<EntityModel> Get(int id);
    }
}
