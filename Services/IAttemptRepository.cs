using QuizApp.Models.Domain;

namespace QuizApp.Services
{
    public interface IAttemptRepository
    {
        Attempt GetAttempt(Guid attemptId);

        void AddAttempt(Attempt attempt);

        void SaveChanges();
    }
}
