using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;

namespace BusinessObjects.Requests
{
    public class GetServiceElementFilterRequest : BasePaginationRequest
    {
        public Guid? Id { get; set; }
        public Guid? ElementTypeId { get; set; }

        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}