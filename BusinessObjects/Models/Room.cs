using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Room : BaseModel
{

    public Guid RoomTypeId { get; set; }

    public int RoomNo { get; set; }

    public int Capacity { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }

    public virtual RoomType RoomType { get; set; } = null!;
}
