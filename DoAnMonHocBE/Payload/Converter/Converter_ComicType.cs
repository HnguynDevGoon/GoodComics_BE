using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.DTO;

namespace DoAnMonHocBE.Payload.Converter
{
    public class Converter_ComicType
    {
        public DTO_ComicType EntityToDTO(ComicType comicType) 
        {
            return new DTO_ComicType
            {
                Id = comicType.Id,
                ComicTypeName = comicType.ComicTypeName,
            };
        }
    }
}
