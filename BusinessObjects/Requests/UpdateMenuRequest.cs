using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdateMenuRequest : BaseUpdateRequest
    {
        public Guid? DishId { get; set; }

        public Guid? ServiceId { get; set; }
    }
}
