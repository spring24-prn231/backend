using AutoFilterer.Types;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class GetRoomFilterRequest : BasePaginationRequest
    {
        public Guid? RoomTypeId { get; set; }
        public Range<int>? RoomNo { get; set; }
        public Range<int>? Capacity { get; set; }
    }
}
