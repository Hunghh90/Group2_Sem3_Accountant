namespace Group2_Sem3_Accountant.Dtos
{
    public class UpdatePayRoll
    {
        public string? Name { get; set; }

        public int? Workday { get; set; }

        public int? PaidLeaver { get; set; }

        public int? UnpaidLeaver { get; set; }

        public int? ActualWorkday { get; set; }

        public decimal? UnionDues { get; set; }

        public decimal? Reduce { get; set; }

        public decimal? Bonus { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
