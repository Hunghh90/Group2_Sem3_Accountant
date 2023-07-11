namespace Group2_Sem3_Accountant.Dtos
{
    public class CreateStaff
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public int PositionId { get; set; }

        public int DepartmentId { get; set; }

        public DateTime JoinDate { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}