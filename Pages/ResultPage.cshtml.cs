using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models.Domain;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class ResultPageModel : PageModel
    {
        [BindProperty]
        public Attempt Attempt { get; set; }

        private IAttemptRepository _attemptRepository;
        public ResultPageModel(IAttemptRepository attemptRepository)
        {
            _attemptRepository = attemptRepository;
        }
        public void OnGet(Guid attemptId)
        {
            Attempt = _attemptRepository.GetAttempt(attemptId);

            var err = Attempt;
        }
    }
}
