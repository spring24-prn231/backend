using BusinessObjects.Models;

namespace Services.Interfaces
{
    public interface INotificationService : IBaseService<Notification>
    {
        Task Confirm(Guid id);
    }
}
