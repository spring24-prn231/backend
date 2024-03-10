using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class GetMenuFilterRequest : BasePaginationRequest
    {
        public Guid? DishId { get; set; }

        public Guid? ServiceId { get; set; }
    }
}
