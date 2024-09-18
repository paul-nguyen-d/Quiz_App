using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.Domain;

namespace QuizApp.Services
{
    public class QuizRepository : IQuizRepository
    {
        private QuizAppDbContext _context;

        public QuizRepository(QuizAppDbContext context)
        {
            _context = context;
            
        }

        public Quiz GetQuizById(Guid id)
        {
            return _context.Quizzes.FirstOrDefault(i => i.Id == id);
        }

        public List<Quiz> GetAllQuizzes()
        {
            return _context.Quizzes.ToList();
        }

        public void AddQuiz(Quiz quiz)
        {
            _context.Add(quiz);
       
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public ICollection<Question> GetQuestionsWithAnswersByQuiz(Guid id)
        {
            return _context.Questions.Include(e => e.Answers).Where(e => e.Quiz.Id == id).ToList();
        }

        public List<Quiz> GetAllQuizzesWithQuestions()
        {
            return _context.Quizzes.Include(q => q.Questions).ToList();
        }
    }
}
