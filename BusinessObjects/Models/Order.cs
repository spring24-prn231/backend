using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Order : BaseModel
{

    public Guid? ServiceId { get; set; }

    public Guid? UserId { get; set; }
    public Guid? StaffId { get; set; }

    public DateTime CreateDate { get; set; }
    public DateTime? EventStart { get; set; }
    public DateTime? EventEnd { get; set; }
    public decimal? MaxGuest { get; set; }

    public decimal? Total { get; set; }
    public string? Name { get; set; }

    public string? Contract { get; set; }

    public virtual ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<PartyPlan> PartyPlans { get; set; } = new List<PartyPlan>();

    public virtual Service? Service { get; set; }

    public virtual ApplicationUser? User { get; set; }
    public virtual ApplicationUser? Staff { get; set; }

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
