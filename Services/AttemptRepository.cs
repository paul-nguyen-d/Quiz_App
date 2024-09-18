using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.Domain;

namespace QuizApp.Services
{
    public class AttemptRepository : IAttemptRepository
    {
        private QuizAppDbContext _context;

        public AttemptRepository(QuizAppDbContext context)
        {
            _context = context;
        }

        public void AddAttempt(Attempt attempt)
        {
            _context.Attempts.Add(attempt);
        }

        public Attempt GetAttempt(Guid attemptId)
        {
            return _context.Attempts.Include(a => a.Responses).ThenInclude(r => r.Question).Where(a => a.Id == attemptId).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
