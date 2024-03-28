using System.ComponentModel.DataAnnotations;

namespace ExamProjectCheck.Models.ViewModels
{
    public class LoginModelss
    {
        [Required]
        [MinLength(2)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
