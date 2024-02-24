using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Slot
{
    public Guid Id { get; set; }

    public Guid? RoomId { get; set; }

    public string FromHour { get; set; } = null!;

    public string ToHour { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Room? Room { get; set; }
}
