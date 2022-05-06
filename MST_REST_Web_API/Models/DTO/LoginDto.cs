using System.ComponentModel.DataAnnotations;

namespace MST_REST_Web_API.Models.DTO
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
