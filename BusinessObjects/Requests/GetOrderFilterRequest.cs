using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;
using BusinessObjects.Common.Constants;

namespace BusinessObjects.Requests
{
    public class GetOrderFilterRequest : BasePaginationRequest
    {
        public Range<int>? ExecutionStatus { get; set; }
        public Guid? ServiceId { get; set; }

        public Guid? UserId { get; set; }
        public Guid? StaffId { get; set; }

        public Range<DateTime>? CreateDate { get; set; }
        public Range<DateTime>? EventStart { get; set; }
        public Range<DateTime>? EventEnd { get; set; }
        public Range<int>? MaxGuest { get; set; }

        public Range<decimal>? Total { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Name { get; set; }
    }
}
