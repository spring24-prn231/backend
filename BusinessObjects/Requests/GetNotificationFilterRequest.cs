using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;

namespace BusinessObjects.Requests
{
    public class GetNotificationFilterRequest : BasePaginationRequest
    {
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Content { get; set; }
        public string? Role { get; set; }
        public bool? Status { get; set; }
    }
}
