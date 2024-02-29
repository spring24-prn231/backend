using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdateDishTypeRequest : BaseUpdateRequest
    {
        public string? Name { get; set; }
    }
}
