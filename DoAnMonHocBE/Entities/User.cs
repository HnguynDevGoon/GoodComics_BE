namespace DoAnMonHocBE.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Urlavartar { get; set; }
        //public bool? Active { get; set; } = false;


        public int? RoleId { get; set; } = 2;
        public Role? Role { get; set; }


        public ICollection<ConfirmEmail>? ConfirmEmails { get; set; }
        public ICollection<Comment>? Comments {  get; set; }
        public ICollection<History>? Historys { get; set; }
        public ICollection<Hobby>? Hobbys { get; set; }
    }
}
