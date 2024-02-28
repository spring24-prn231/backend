using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class DishType : BaseModel
{

    public string? Name { get; set; }

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
