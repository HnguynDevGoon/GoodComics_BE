namespace DoAnMonHocBE.Entities
{
    public class Hobby : BaseEntity
    {
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? ComicId { get; set; }
        public Comic? Comic { get; set; }
    }
}
