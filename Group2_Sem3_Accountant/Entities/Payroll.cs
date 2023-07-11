using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class Payroll
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Workday { get; set; }

    public int? PaidLeaver { get; set; }

    public int? UnpaidLeaver { get; set; }

    public decimal BaseSalary { get; set; }

    public decimal TotalSalary { get; set; }

    public decimal? Reduce { get; set; }

    public decimal ActualWorkingDaySalary { get; set; }

    public decimal? Allowance { get; set; }

    public int? StaffId { get; set; }

    public int? UserId { get; set; }

    public byte Status { get; set; }

    public int? ActualWorkday { get; set; }

    public decimal? UnionDues { get; set; }

    public decimal? Insurance { get; set; }

    public decimal? Bonus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UserCreateId { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual User? User { get; set; }

    public virtual User? UserCreate { get; set; }
}
