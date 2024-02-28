using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Service : BaseModel
{

    public Guid RoomTypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid? UserId { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual RoomType RoomType { get; set; } = null!;

    public virtual ICollection<ServiceElementDetail> ServiceElementDetails { get; set; } = new List<ServiceElementDetail>();
}
