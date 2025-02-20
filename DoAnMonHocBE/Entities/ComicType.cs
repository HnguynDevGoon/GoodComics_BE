namespace DoAnMonHocBE.Entities
{
    public class ComicType : BaseEntity
    {
        public string ComicTypeName { get; set; }

        public ICollection<Comic>? Comics { get; set; }
    }
}
