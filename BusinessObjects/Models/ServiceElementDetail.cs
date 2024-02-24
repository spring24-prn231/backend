using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class ServiceElementDetail
{
    public Guid Id { get; set; }

    public Guid ServiceElementId { get; set; }

    public Guid ServiceId { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual ServiceElement ServiceElement { get; set; } = null!;
}
