using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models.Domain;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class QuizzesModel : PageModel
    {
        private IQuizRepository _quizRepository;
        private List<Quiz> _quizzes;
        public QuizzesModel(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public void OnGet()
        {
            _quizzes = _quizRepository.GetAllQuizzesWithQuestions();
        }

        public List<Quiz> Quizzes
        {
            get
            {
                return _quizzes;
            }
        }
    }
}
