using System;
using System.Collections.Generic;

namespace Group2_Sem3_Accountant.Entities;

public partial class Financein
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    public string? Document { get; set; }

    public int? UserId { get; set; }

    public int? TypefinanceinId { get; set; }

    public byte? Status { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UserCreateId { get; set; }

    public virtual Typefinancein? Typefinancein { get; set; }

    public virtual User? User { get; set; }

    public virtual User? UserCreate { get; set; }
}
