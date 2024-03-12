using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class CreateMenuRequest
    {
        public Guid DishId { get; set; }

        public Guid ServiceId { get; set; }
    }
}
