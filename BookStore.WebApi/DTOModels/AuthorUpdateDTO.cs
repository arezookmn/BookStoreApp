﻿namespace BookStore.WebApi.DTOModels
{
    public class AuthorUpdateDTO
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Bio { get; set; }
    }
}
