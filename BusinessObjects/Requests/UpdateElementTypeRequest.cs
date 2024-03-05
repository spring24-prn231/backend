using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdateElementTypeRequest : BaseUpdateRequest
    {
        public string? Name { get; set; }
    }
}