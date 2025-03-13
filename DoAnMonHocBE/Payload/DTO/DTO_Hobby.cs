namespace DoAnMonHocBE.Payload.DTO
{
    public class DTO_Hobby
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public int? ComicId { get; set; }
        public string UrlImg { get; set; }
        public string ComicName { get; set; }
        public string ComicAuthor { get; set; }
        public string ComicTypeName { get; set; }
    }
}


