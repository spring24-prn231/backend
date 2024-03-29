using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class CreateServiceElementRequest
    {
        public Guid ElementTypeId { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public IFormFile ImageFile { get; set; } = null!;
    }
}