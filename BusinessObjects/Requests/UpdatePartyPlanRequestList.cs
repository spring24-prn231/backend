using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdatePartyPlanRequestList
    {
        public Guid? Id { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        public string? Description { get; set; }

        public string? Note { get; set; }
        public string? Feedback { get; set; }
    }
}
