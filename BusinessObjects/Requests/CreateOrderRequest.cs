using BusinessObjects.Common.Constants;

namespace BusinessObjects.Requests
{
    public class CreateOrderRequest
    {
        public ServiceOrderCreate? NewService { get; set; }
        public Guid? RecommendServiceId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public int MaxGuest { get; set; }
        public decimal? Total { get; set; } = 0;
        public string Name { get; set; } = null!;
    }
    public class ServiceOrderCreate
    {
        public Guid RoomTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public List<Guid> ServiceElementIds { get; set; } = null!;

        public List<Guid> DishIds { get; set; } = null!;
    }

}
