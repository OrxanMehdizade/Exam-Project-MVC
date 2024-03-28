using Microsoft.AspNetCore.Identity;

namespace ExamProjectCheck.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
