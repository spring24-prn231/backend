using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Requests
{
    public class UpdateVoucherRequest : BaseUpdateRequest
    {
        public string Code { get; set; }
        public int? Discount { get; set; }
        public decimal? MaximumValue { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}
