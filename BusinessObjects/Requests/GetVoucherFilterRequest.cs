using AutoFilterer.Attributes;
using AutoFilterer.Types;
using AutoFilterer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessObjects.Requests
{
    public class GetVoucherFilterRequest : BasePaginationRequest
    {
        public Guid? Id { get; set; }
        public Guid? OrderId { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Code { get; set; }
        public Range<int>? Discount { get; set; }
        public Range<decimal>? MaximumValue { get; set; }
        public Range<DateTime>? ExpirationDate { get; set; }
    }
}
