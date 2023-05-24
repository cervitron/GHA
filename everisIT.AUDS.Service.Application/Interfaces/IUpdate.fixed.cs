namespace everisIT.AUDS.Service.Application.Interfaces
{
    public partial interface IUpdate<EntityDto>
    {
        System.Threading.Tasks.Task<EntityDto> Update(EntityDto dataDto);
    }
}
