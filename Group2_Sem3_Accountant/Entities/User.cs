using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public byte? Status { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? Permission { get; set; }

    public virtual ICollection<Financein> Financeins { get; set; } = new List<Financein>();

    public virtual ICollection<Financeout> Financeouts { get; set; } = new List<Financeout>();
}
