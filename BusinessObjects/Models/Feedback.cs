using AutoFilterer.Attributes;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;
public partial class Feedback : BaseModel
{

    public Guid? OrderId { get; set; }

    public byte? RatingStar { get; set; }

    public string? Comment { get; set; }

    public DateTime ModifiedDate { get; set; }
    public virtual Order? Order { get; set; }
}
