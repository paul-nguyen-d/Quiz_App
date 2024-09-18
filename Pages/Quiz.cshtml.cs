using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models.Domain;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class QuizModel : PageModel
    {
        private IQuizRepository _quizRepository;
        private IQuestionRepository _questionRepository;

        public Quiz Quiz { get; set; }

        public ICollection<Question> Questions { get; set; }
        public QuizModel(IQuizRepository quizRepository, IQuestionRepository questionRepository)
        {
            _quizRepository = quizRepository;
            _questionRepository = questionRepository;
        }
        public void OnGet(Guid id)
        {
            Quiz = _quizRepository.GetQuizById(id);
            Questions = _questionRepository.GetQuestionsByQuiz(id);
        }
    }
}
