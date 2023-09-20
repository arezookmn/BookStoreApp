using BookStore.WebApi.DTOModels.BookDTO;

namespace BookStore.WebApi.DTOModels.AuthorDTO
{
    public class AuthorDetailsDTO : AuthorResponseDTO
    {
        public List<BookResponseDTO> Books { get; set; }
    }
}
