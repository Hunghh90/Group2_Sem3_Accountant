using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class Fund
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Amount { get; set; }
}
