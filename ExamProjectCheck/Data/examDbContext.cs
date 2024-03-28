using ExamProjectCheck.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExamProjectCheck.Data
{
    public class examDbContext : IdentityDbContext<AppUser>
    {
        public examDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();
    }
}
