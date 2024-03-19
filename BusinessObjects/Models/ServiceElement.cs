using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class ServiceElement : BaseModel
{

    public Guid ElementTypeId { get; set; }

    public string? Description { get; set; }
    public string? Name { get; set; }

    public string? Image { get; set; }
    public decimal? Price { get; set; }

    public virtual ElementType ElementType { get; set; } = null!;

    public virtual ICollection<ServiceElementDetail> ServiceElementDetails { get; set; } = new List<ServiceElementDetail>();
}
