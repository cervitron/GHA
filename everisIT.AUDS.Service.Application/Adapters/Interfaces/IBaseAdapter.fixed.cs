using System.Collections.Generic;

namespace everisIT.AUDS.Service.Application.Adapters.Interfaces
{
    public partial interface IBaseAdapter<EntityDto, Entity>
    {
        Entity Map(EntityDto entityDto);

        EntityDto Map(Entity entity);

        List<Entity> Map(List<EntityDto> listDto);

        IList<EntityDto> Map(IList<Entity> list);
    }
}
