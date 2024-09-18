using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.Domain;

namespace QuizApp.Services
{
    public class QuestionRepository : IQuestionRepository
    {
        private QuizAppDbContext _context;

        public QuestionRepository(QuizAppDbContext context)
        {
            _context = context;
        }

        public void Delete(Question question)
        {
            try
            {
                _context.Questions.Remove(question);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
        }

        public void DeleteById(Guid id)
        {
            var question = _context.Questions.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                _context.Questions.Remove(question);
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
        }

        public Question GetQuestion(Guid questionId)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == questionId);
        }

        public ICollection<Question> GetQuestionsByQuiz(Guid quizId)
        {
            return _context.Questions.Include(q => q.Answers).Where(q => q.Quiz.Id == quizId).ToList();
        }

        public Question GetQuestionWithAnswers(Guid questionId)
        {
            return _context.Questions.Include(q => q.Answers).FirstOrDefault(q => q.Id == questionId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
