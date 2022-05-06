using System.ComponentModel.DataAnnotations;

namespace MST_REST_Web_API.Models.DTO
{
    public class NewPasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
