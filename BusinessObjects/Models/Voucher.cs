using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Voucher : BaseModel
{

    public Guid? OrderId { get; set; }

    public string? Code { get; set; }

    public int? Discount { get; set; }

    public decimal? MaximumValue { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual Order? Order { get; set; }
}
