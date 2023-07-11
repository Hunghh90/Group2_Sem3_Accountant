namespace Group2_Sem3_Accountant.Dtos
{
    public class UpdateAdvance
    {
        public DateTime? DateOfAdvances { get; set; }

        public decimal? Amount { get; set; }

        public string? Description { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
