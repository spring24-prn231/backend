﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class GetSlotFilterRequest : BasePaginationRequest
    {
        public Guid? RoomId { get; set; }

        public string? FromHour { get; set; } 

        public string? ToHour { get; set; } 
    }
}