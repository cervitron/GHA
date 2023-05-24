namespace everisIT.AUDS.Service.Application.Interfaces
{
    public partial interface IDelete<EntityDto>
    {
        System.Threading.Tasks.Task<EntityDto> Delete(int id);
    }
}
