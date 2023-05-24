namespace everisIT.AUDS.Service.Application.Interfaces
{
    public partial interface ICreate<EntityDto>
    {
        System.Threading.Tasks.Task<EntityDto> Create(EntityDto dataDto);
    }
}
