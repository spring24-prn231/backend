using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class OrderDetail
{
    public Guid Id { get; set; }

    public Guid? OrderId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Price { get; set; }

    public string? Type { get; set; }

    public decimal? Cost { get; set; }

    public virtual Order? Order { get; set; }
}
