using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class Typefinancein
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte? Status { get; set; }

    public virtual ICollection<Financein> Financeins { get; set; } = new List<Financein>();
}
