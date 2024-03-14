using Microsoft.AspNetCore.Http;

namespace BusinessObjects.Requests
{
    public class CreateDishRequest
    {
        public Guid DishTypeId { get; set; }
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
    }
}