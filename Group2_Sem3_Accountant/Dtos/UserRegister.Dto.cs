﻿namespace Group2_Sem3_Accountant.Dtos
{
    public class UserRegister
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Role { get; set; }
        public string Permission { get; set; }
        

    }
}
