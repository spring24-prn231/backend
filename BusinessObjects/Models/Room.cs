using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Room
{
    public Guid Id { get; set; }

    public Guid RoomTypeId { get; set; }

    public int RoomNo { get; set; }

    public int Capacity { get; set; }

    public virtual RoomType RoomType { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
