using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class Advance
{
    public int Id { get; set; }

    public DateTime DateOfAdvances { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = null!;

    public byte? Status { get; set; }

    public int StaffId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UserId { get; set; }

    public int? UserCreateId { get; set; }

    public virtual Staff Staff { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual User? UserCreate { get; set; }
}
