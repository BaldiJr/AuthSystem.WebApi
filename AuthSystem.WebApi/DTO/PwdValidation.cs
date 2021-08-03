using System.ComponentModel.DataAnnotations;

namespace AuthSystem.WebApi.DTO
{
    public class PwdValidation
    {
        [Required]
        public string Password { get; set; }
    }
}
