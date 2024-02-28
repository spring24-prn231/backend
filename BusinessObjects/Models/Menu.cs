using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Menu : BaseModel
{

    public Guid? DishId { get; set; }

    public Guid? ServiceId { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual Service? Service { get; set; }
}
