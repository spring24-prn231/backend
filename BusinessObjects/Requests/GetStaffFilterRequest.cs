using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class GetStaffFilterRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool GetHostStaff { get; set; } = true;
        public bool GetImplementStaff { get; set; } = true;

    }
}