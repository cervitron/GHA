using everisIT.AUDS.Service.Application.Adapters.Interfaces;
using System.Collections.Generic;

namespace everisIT.AUDS.Service.Application.Adapters
{
    public abstract partial class BaseAdapter<EntityDto, Entity> : IBaseAdapter<EntityDto, Entity>
    {
        public abstract Entity Map(EntityDto entityDto);

        public abstract EntityDto Map(Entity entity);

        public abstract List<Entity> Map(List<EntityDto> listDto);

        public abstract IList<EntityDto> Map(IList<Entity> list);
    }
}
