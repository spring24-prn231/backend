using Microsoft.AspNetCore.Http;

namespace BusinessObjects.Requests
{
    public class UpdateOrderRequest : BaseUpdateRequest
    {
        public Guid? ServiceId { get; set; }

        public DateTime? EventStart { get; set; }
        public DateTime? EventEnd { get; set; }
        public int? MaxGuest { get; set; }

        public decimal? Total { get; set; }
        public string? Name { get; set; }

        public IFormFile? ContractFile { get; set; }
    }
}
