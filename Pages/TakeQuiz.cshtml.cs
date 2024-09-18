using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models.Domain;
using QuizApp.Models.ViewModels;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class TakeQuizModel : PageModel
    {
        private IQuestionRepository _questionRepository;
        private IAttemptRepository _attemptRepository;

        public ICollection<Question> Questions { get; set; }

        [BindProperty]
        public List<AddResponseRequest> Responses { get; set; }

        public TakeQuizModel(IQuestionRepository questionRepository, IAttemptRepository attemptRepository)
        {
            _questionRepository = questionRepository;
            _attemptRepository = attemptRepository;
            Responses = new List<AddResponseRequest>();
        }
        public void OnGet(Guid quizId)
        {
            Questions = _questionRepository.GetQuestionsByQuiz(quizId).ToList();
            foreach(var question in Questions)
            {
                Responses.Add(new AddResponseRequest());

            }
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return Redirect("/Error");
            }

            var attempt = new Attempt
            {
                Id = Guid.NewGuid(),
                Responses = new List<UserResponse>()
            };

            foreach(var response in Responses)
            {
                var question = _questionRepository.GetQuestionWithAnswers(response.QuestionId);
                var answer = question.Answers.Where(ans => ans.Id == response.AnswerId).FirstOrDefault();
                var userResponse = new UserResponse
                {
                    Id = Guid.NewGuid(),
                    Question = question,
                    Answer = answer,
                    Timestamp = DateTime.Now,
                    IsCorrect = (question.CorrectAnswerId == answer.Id)
                };

                attempt.Responses.Add(userResponse);
            }

            _attemptRepository.AddAttempt(attempt);

            _attemptRepository.SaveChanges();


            return Redirect($"/ResultPage/{attempt.Id}");
        }
    }
}
