namespace Group2_Sem3_Accountant.Dtos
{
    public class CreateExpense
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string? Document { get; set; }

        public int TypefinanceoutId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
