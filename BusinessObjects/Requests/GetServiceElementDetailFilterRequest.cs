using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class GetServiceElementDetailFilterRequest : BasePaginationRequest
    {
        public Guid? Id { get; set; }
        
        public Guid? ServiceElementId { get; set; }

        public Guid? ServiceId { get; set; }
    }
}