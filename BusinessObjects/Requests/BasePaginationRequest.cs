using AutoFilterer.Attributes;
using AutoFilterer.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class BasePaginationRequest : OrderableFilterBase
    {
        public Guid? Id { get; set; }

        [IgnoreFilter]
        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;
        [IgnoreFilter]
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 10;
        [IgnoreFilter]
        public bool IsEager { get; set; } = true;
    }
}
