﻿using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class GetDishFilterRequest : BasePaginationRequest
    {
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}