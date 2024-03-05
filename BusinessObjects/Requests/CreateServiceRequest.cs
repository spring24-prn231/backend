using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class CreateServiceRequest
    {
        public Guid RoomTypeId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public Guid UserId { get; set; }
    }
}