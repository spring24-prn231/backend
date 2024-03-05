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
        [MaxLength(20)]
        public string? Code { get; set; }
        [Range(0, int.MaxValue)]
        public int? Discount { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? MaximumValue { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}
