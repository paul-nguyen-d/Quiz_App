using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models.Domain;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class AddQuizModel : PageModel
    {
        [BindProperty]
        public string QuizTitle { get; set; }
        [BindProperty]
        public string QuizDescription { get; set; }

        private IQuizRepository _quizRepository;
        public AddQuizModel(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var quiz = new Quiz
            {
                Title = QuizTitle,
                Description = QuizDescription,
            };

            _quizRepository.AddQuiz(quiz);
            _quizRepository.SaveChanges();
            return Redirect("/Quizzes");
        }
    }
}
