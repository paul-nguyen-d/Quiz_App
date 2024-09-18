using QuizApp.Models.Domain;

namespace QuizApp.Services
{
    public interface IQuizRepository
    {
        public List<Quiz> GetAllQuizzes();
        public List<Quiz> GetAllQuizzesWithQuestions();
        public Quiz GetQuizById(Guid id);
        public void AddQuiz(Quiz quiz);

        void SaveChanges();
    }
}
