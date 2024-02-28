using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class RoomType : BaseModel
{

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
