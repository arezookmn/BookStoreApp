using AutoMapper;
using BookStore.WebApi.DTOModels.AuthorDTO;
using BookStore.WebApi.DTOModels.BookDTO;
using BookStore.WebApi.Models;

namespace BookStore.WebApi.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorResponseDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();

            CreateMap<BookCreateDTO, Book>().ReverseMap();
            CreateMap<Book, BookResponseDTO>()
                .ForMember( t => t.AuthorName, t => t.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();

        }

    }
}
