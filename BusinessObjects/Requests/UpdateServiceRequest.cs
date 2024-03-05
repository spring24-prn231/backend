using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdateServiceRequest : BaseUpdateRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<Guid> ServiceElementIds { get; set; } = new List<Guid>();

        public List<Guid> DishIds { get; set; } = new List<Guid>();
    }
}