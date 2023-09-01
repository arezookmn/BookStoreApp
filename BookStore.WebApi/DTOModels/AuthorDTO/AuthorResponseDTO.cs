namespace BookStore.WebApi.DTOModels.AuthorDTO
{
    public class AuthorResponseDTO
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Bio { get; set; }
    }
}
