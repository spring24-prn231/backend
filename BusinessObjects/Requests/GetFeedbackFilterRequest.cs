using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;

namespace BusinessObjects.Requests
{
    public class GetFeedbackFilterRequest : BasePaginationRequest
    {
        public Guid? Id { get; set; }
        public Guid? OrderId { get; set; }
        public Range<byte>? RatingStar { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Comment { get; set; }
        public Range<DateTime>? ModifiedDate { get; set; }
    }
}
