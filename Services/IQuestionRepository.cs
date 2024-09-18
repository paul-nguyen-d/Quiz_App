using QuizApp.Models.Domain;

namespace QuizApp.Services
{
    public interface IQuestionRepository
    {
        public Question GetQuestion(Guid questionId);
        public Question GetQuestionWithAnswers(Guid questionId);
        public ICollection<Question> GetQuestionsByQuiz(Guid quizId);

        public void DeleteById(Guid id);

        public void Delete(Question question);

        public void SaveChanges();
    }
}
