namespace everisIT.AUDS.Service.Application.Interfaces
{
    public partial interface IGet<EntityDto>
    {
        System.Threading.Tasks.Task<EntityDto> Get(int id);
    }
}
