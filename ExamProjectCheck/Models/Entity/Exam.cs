namespace ExamProjectCheck.Models.Entity
{
    public class Exam:BaseEntity
    {
        public string Name { get; set; }
        public double TotalScore { get; set; }
        public double GPA { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }

    }
}
