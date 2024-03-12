using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class CreateServiceElementRequest
    {
        public Guid ElementTypeId { get; set; }

        public string? Description { get; set; }

        public string Image { get; set; }
    }
}