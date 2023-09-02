using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApi.DTOModels.UserDTO
{
    public class UserRegisterDTO
    {
        [Required]
        [EmailAddress]  
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
