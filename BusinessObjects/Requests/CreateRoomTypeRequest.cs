﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class CreateRoomTypeRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
