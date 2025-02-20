namespace DoAnMonHocBE.Entities
{
    public class ConfirmEmail : BaseEntity
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Expiredtime { get; set; }


        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
