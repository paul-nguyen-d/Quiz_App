using Microsoft.EntityFrameworkCore;
using QuizApp.Models.Domain;

namespace QuizApp.Data
{
    public class QuizAppDbContext : DbContext
    {
        public QuizAppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserResponse> UserResponses { get; set; }
        public DbSet<Attempt> Attempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //1-to-many relationship config
            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers);
        }

    }
}
