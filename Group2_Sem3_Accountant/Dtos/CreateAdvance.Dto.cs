namespace Group2_Sem3_Accountant.Dtos
{
    public class CreateAdvance
    {
        public DateTime DateOfAdvances { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public int StaffId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
