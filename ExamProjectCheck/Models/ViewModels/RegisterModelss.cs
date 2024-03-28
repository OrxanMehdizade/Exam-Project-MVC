using System.ComponentModel.DataAnnotations;

namespace ExamProjectCheck.Models.ViewModels
{
    public class RegisterModelss
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(5)]
        public string FullName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
