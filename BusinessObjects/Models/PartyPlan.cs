using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class PartyPlan : BaseModel
{

    public Guid? OrderId { get; set; }

    public DateTime? TimeStart { get; set; }

    public DateTime? TimeEnd { get; set; }

    public string? Description { get; set; }

    public string? Note { get; set; }
    public string? Feedback { get; set; }

    public virtual Order? Order { get; set; }
}
