using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class Totalsalarysheet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
