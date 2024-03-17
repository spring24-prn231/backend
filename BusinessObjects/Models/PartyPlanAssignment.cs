using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class PartyPlanAssignment : BaseModel
    {
        public Guid PartyPlanId { get; set; }
        public Guid StaffId { get; set; }

        public virtual PartyPlan? PartyPlan { get; set; }
        public virtual ApplicationUser? Staff { get; set; }
    }
}
