using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class AssignStaffRequest
    {
        public Guid OrderId { get; set; }
        public Guid StaffId { get; set; }
    }
}
