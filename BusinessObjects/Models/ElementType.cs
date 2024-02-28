using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class ElementType : BaseModel
{

    public string? Name { get; set; }

    public virtual ICollection<ServiceElement> ServiceElements { get; set; } = new List<ServiceElement>();
}
