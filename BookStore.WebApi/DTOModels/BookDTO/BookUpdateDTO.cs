namespace BookStore.WebApi.DTOModels.BookDTO
{
    public class BookUpdateDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string Isbn { get; set; } = null!;

        public string? Summery { get; set; }

        public int? Year { get; set; }

        public string? Image { get; set; }

        public decimal? Price { get; set; }

        public int AuthorId { get; set; }
    }
}
