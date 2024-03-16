using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class GetOrderDetailFilterRequest : BasePaginationRequest
    {
        public Guid? OrderId { get; set; }

        public Range<decimal>? Amount { get; set; }

        public Range<decimal>? Price { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Type { get; set; }

        public Range<decimal>? Cost { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Note { get; set; }
    }
}
