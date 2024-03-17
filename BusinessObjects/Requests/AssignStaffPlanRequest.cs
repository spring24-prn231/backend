using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class AssignStaffPlanRequest
    {
        public Guid PlanId { get; set; }
        public List<Guid> StaffIds { get; set; } = null!;
    }
}
