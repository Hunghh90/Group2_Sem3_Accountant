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

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Advance> AdvanceUserCreates { get; set; } = new List<Advance>();

    public virtual ICollection<Advance> AdvanceUsers { get; set; } = new List<Advance>();

    public virtual ICollection<Financein> FinanceinUserCreates { get; set; } = new List<Financein>();

    public virtual ICollection<Financein> FinanceinUsers { get; set; } = new List<Financein>();

    public virtual ICollection<Financeout> FinanceoutUserCreates { get; set; } = new List<Financeout>();

    public virtual ICollection<Financeout> FinanceoutUsers { get; set; } = new List<Financeout>();

    public virtual ICollection<Payroll> PayrollUserCreates { get; set; } = new List<Payroll>();

    public virtual ICollection<Payroll> PayrollUsers { get; set; } = new List<Payroll>();
}
