using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdatePartyPlanRequestList
    {
        public Guid OrderId { get; set; }
        public List<UpdatePartyPlanDetailRequest> PartyPlans { get; set; } = null!;
    }
}
