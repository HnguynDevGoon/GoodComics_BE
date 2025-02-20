namespace DoAnMonHocBE.Entities
{
    public class Comment : BaseEntity
    {
        public string CommentTitle { get; set; }
        public int Rate { get; set; }

        // ---------
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? ComicId { get; set; }
        public Comic? Comic { get; set; }
    }
}
