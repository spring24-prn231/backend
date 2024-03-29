﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Deposit : BaseModel
{

    public Guid? OrderId { get; set; }

    public decimal? Value { get; set; }

    public string? Note { get; set; }

    public virtual Order? Order { get; set; }
}
