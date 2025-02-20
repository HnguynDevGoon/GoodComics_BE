namespace DoAnMonHocBE.Entities
{
    public class History : BaseEntity
    {
        public DateTime LastRead { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? ComicId { get; set; }
        public Comic? Comic { get; set; }

    }
}
