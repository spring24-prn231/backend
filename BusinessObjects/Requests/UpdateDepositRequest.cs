using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdateDepositRequest : BaseUpdateRequest
    {
        public decimal? Value { get; set; }

        public string? Note { get; set; }
    }
}