using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<Order> UserOrders {  get; set; } = new List<Order>(); 
        public virtual ICollection<Order> StaffOrders {  get; set; } = new List<Order>(); 
    }
}
