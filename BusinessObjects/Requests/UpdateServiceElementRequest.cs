using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdateServiceElementRequest : BaseUpdateRequest
    {
        public Guid? ElementTypeId { get; set; }

        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}