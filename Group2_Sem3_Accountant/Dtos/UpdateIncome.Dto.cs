namespace Group2_Sem3_Accountant.Dtos
{
    public class UpdateIncome
    {
        public string? Name { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public string? Document { get; set; }

        public int? TypefinanceinId { get; set; }

        public decimal? Amount { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
