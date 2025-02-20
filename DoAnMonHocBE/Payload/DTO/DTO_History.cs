namespace DoAnMonHocBE.Payload.DTO
{
    public class DTO_History
    {
        public int Id { get; set; }
        public DateTime LastRead { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public int? ComicId { get; set; }
        public string ComicName { get; set; }
    }
}
