using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFilterer.Attributes;
using AutoFilterer.Enums;

namespace BusinessObjects.Requests
{
    public class GetElementTypeFilterRequest : BasePaginationRequest
    {
        public Guid? Id { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Name { get; set; }
    }
}