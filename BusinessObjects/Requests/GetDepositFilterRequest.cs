using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;

namespace BusinessObjects.Requests
{
    public class GetDepositFilterRequest : BasePaginationRequest
    {
        public Guid? OrderId { get; set; }

        public Range<decimal>? Value { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Note { get; set; }

    }
}