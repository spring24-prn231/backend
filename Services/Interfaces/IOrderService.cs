using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace Services.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
        Task Create<TReq>(TReq entity, Guid userId);
        Task AssignStaff(AssignStaffRequest request, string staffUserName);
        Task<bool> DoneOrder(DoneOrderRequest request, Guid staffId);
    }
}
