using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class CreateRoomRequest
    {
        public Guid RoomTypeId { get; set; }

        public int RoomNo { get; set; }

        public int Capacity { get; set; }
        public string? Name { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
