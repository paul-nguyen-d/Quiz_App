using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models.Domain;
using QuizApp.Services;

namespace QuizApp.Pages
{
    public class AddQuestionModel : PageModel
    {
        [BindProperty]
        public string QuestionContent { get; set; }

        [BindProperty]
        public int CorrectAnswerIndex { get; set; }

        [BindProperty]
        public Guid QuizId { get; set; }

        [BindProperty]
        public string Answer1 { get; set; }

        [BindProperty]
        public string Answer2 { get; set; }

        [BindProperty]
        public string Answer3 { get; set; }

        [BindProperty]
        public string Answer4 { get; set; }

        private IQuizRepository _quizRepository;
        public AddQuestionModel(IQuizRepository repository)
        {
            _quizRepository = repository;
     
        }

        public void OnGet(Guid quizId)
        {
            try
            {
                var quiz = _quizRepository.GetQuizById(quizId);

                if(quiz==null)
                {
                    Console.WriteLine("Quiz is null");
                }
                else
                {
                    QuizId = quiz.Id;
                }
               
            }
            catch(Exception e)
            {
                Console.WriteLine($"Failed in querying quiz with Id {quizId}: {e.Message}");
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return Page();
            }

            //Query quiz
            var quiz = _quizRepository.GetQuizById(QuizId);

            if (quiz == null)
            {
                return RedirectToPage("/Error");
            }

            //Create new question
            var question = new Question
            {
                Content = QuestionContent,
                Quiz = quiz,
                Answers = new List<Answer>
                {
                    new Answer {Content = Answer1},
                    new Answer {Content = Answer2},
                    new Answer {Content = Answer3},
                    new Answer {Content = Answer4}
                }
            };

            

            try
            {
                quiz.Questions.Add(question);
                _quizRepository.SaveChanges();

                //Add correct answer reference

                if (CorrectAnswerIndex >= 0 && CorrectAnswerIndex < question.Answers.Count)
                {
                    question.CorrectAnswerId = question.Answers.ElementAt(CorrectAnswerIndex).Id;
                    _quizRepository.SaveChanges();
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"There is some error in adding question to the quiz with ID {quiz.Id}, error: {e.Message}");
            }
           
            return Redirect($"/Quiz/{QuizId}");
        }
    }
}
