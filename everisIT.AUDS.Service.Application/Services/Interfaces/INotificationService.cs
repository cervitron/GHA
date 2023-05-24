using everisIT.AUDS.Service.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface INotificationService
    {
        Task<IList<MessageDto>> NotificationsMitigation();
        Task<IList<MessageDto>> NotificationsResponsible();
        Task<IList<MessageDto>> NotificationsUpdate();
        Task<IList<MessageDto>> NotificationEscalation();
        Task<IList<MessageDto>> NotificationEndFollowUp();
    }
}
