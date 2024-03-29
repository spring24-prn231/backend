﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Dish : BaseModel
{

    public Guid DishTypeId { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }
    public decimal? Price { get; set; }

    public string? Description { get; set; }
    public virtual DishType DishType { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
