using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class Position
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Coefficient { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
