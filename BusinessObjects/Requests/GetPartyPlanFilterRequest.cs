﻿using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class GetPartyPlanFilterRequest : BasePaginationRequest
    {
        public Guid? OrderId { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Description { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Note { get; set; }
        [ToLowerContainsComparison]
        [StringFilterOptions(StringFilterOption.Contains, StringComparison.InvariantCultureIgnoreCase)]
        public string? Feedback { get; set; }
    }
}
