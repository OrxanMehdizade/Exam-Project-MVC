using ExamProjectCheck.Data;
using ExamProjectCheck.Models.Entity;
using ExamProjectCheck.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExamProjectCheck.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly examDbContext _examDbContext;

        public ExamController(examDbContext examDbContext)
        {
            _examDbContext = examDbContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var exam= _examDbContext.Exams.ToList();
            return View(exam);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddExam()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExam(ExamModelss model)
        {
            if (ModelState.IsValid)
            {
                var totalScore = CalculateAttendanceScore(model);
                var gpa = CalculateGPA(model);

                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var exam = new Exam
                {
                    Name = model.Name,
                    TotalScore = totalScore,
                    GPA = gpa,
                    UserId = userId
                };

                _examDbContext.Exams.Add(exam);
                await _examDbContext.SaveChangesAsync();

                return RedirectToAction("GetAll");

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ViewRanking()
        {
            var usersWithScores = _examDbContext.AppUsers
                .Include(u => u.Exams)
                .Select(u => new { Name = u.FullName, AverageExamScore = u.Exams.Average(e => e.TotalScore) })
                .OrderByDescending(u => u.AverageExamScore)
                .ToList();

            return View(usersWithScores);
        }
        private double CalculateAttendanceScore(ExamModelss model)
        {
            if (model.TotalClassHours == 0)
            {
                return 0.0;
            }

            double attendanceRate = 1.0 - ((double)model.MissedClasses / model.TotalClassHours);

            double attendanceScore = attendanceRate * 100.0;

            attendanceScore = Math.Max(0.0, Math.Min(attendanceScore, 100.0));

            return attendanceScore;
        }

        private double CalculateGPA(ExamModelss model)
        {
            double totalScore = CalculateAttendanceScore(model);
            double gpa;

            if (totalScore >= 90)
            {
                gpa = 4.0;
            }
            else if (totalScore >= 80)
            {
                gpa = 3.5; 
            }
            else if (totalScore >= 70)
            {
                gpa = 3.0;
            }
            else if (totalScore >= 60)
            {
                gpa = 2.5;
            }
            else if (totalScore >= 50)
            {
                gpa = 2.0;
            }
            else
            {
                gpa = 0.0;
            }

            return gpa;
        }


    }
}
