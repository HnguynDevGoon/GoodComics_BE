namespace DoAnMonHocBE.Payload.DTO
{
    public class DTO_Comic
    {
        public int Id {  get; set; }
        public string ComicName { get; set; }
        public string ComicContent { get; set; }
        public string ComicAuthor { get; set; }
        public DateTime CreateDate { get; set; }
        public string UrlImage { get; set; }
        public int? ComicTypeId { get; set; }
        public string ComicTypeName { get; set; }
    }
}
