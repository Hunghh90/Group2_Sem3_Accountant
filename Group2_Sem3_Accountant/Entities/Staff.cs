using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class Staff
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? Birthday { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Telephone { get; set; }

    public byte? Status { get; set; }

    public int PositionId { get; set; }

    public int DepartmentId { get; set; }

    public DateTime JoinDate { get; set; }

    public virtual ICollection<Advance> Advances { get; set; } = new List<Advance>();

    public virtual Department? Department { get; set; } = null!;

    public virtual Position? Position { get; set; } = null!;
}
