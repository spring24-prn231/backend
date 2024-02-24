using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class AppResponse<TData>
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public TData Data { get; set; }
    }
}
