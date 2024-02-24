using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class ElementType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ServiceElement> ServiceElements { get; set; } = new List<ServiceElement>();
}
