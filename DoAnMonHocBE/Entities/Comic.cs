using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnMonHocBE.Entities
{
    public class Comic : BaseEntity
    {
        public string ComicName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ComicContent { get; set; }

        public string ComicAuthor { get; set; }
        public DateTime CreateDate { get; set; }
        public string UrlImage { get; set; }
        

        public int? ComicTypeId { get; set; }
        public ComicType? ComicType { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<History>? Historys { get; set; }
        public ICollection<Hobby>? Hobbys { get; set; }
    }
}
