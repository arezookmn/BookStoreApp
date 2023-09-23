using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApi.DTOModels.BookDTO
{
    public class BookCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }

        [Required]
        public string Isbn { get; set; } = null!;

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string? Summery { get; set; }

        [Required]
        [Range(1800, int.MaxValue)]
        public int? Year { get; set; }

        public string? ImageData { get; set; }
        public string? OriginalImageName { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public decimal? Price { get; set; }

        public int AuthorId { get; set; }
    }
}
