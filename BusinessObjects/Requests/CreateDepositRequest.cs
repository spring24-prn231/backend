using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class CreateDepositRequest
    {
        public Guid OrderId { get; set; }
        public decimal? Value { get; set; }

        public string? Note { get; set; }
    }
}