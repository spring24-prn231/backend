﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class OrderDetail : BaseModel
{

    public Guid? OrderId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Price { get; set; }

    public string? Type { get; set; }

    public decimal? Cost { get; set; }
    public string? Note { get; set; }

    public virtual Order? Order { get; set; }
}
