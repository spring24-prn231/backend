using AutoFilterer.Types;

namespace BusinessObjects.Requests
{
    public class GetRoomFilterRequest : BasePaginationRequest
    {
        public Guid? RoomTypeId { get; set; }

        public Range<int>? RoomNo { get; set; }

        public Range<int>? Capacity { get; set; }
        public Range<decimal>? Price { get; set; }
    }
}
