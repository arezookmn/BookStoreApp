using AutoMapper;
using BookStore.WebApi.DTOModels;
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
        }

    }
}
