using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class CreateOrderDetailRequest
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public decimal Cost { get; set; }
        public string Note { get; set; }
    }
}
