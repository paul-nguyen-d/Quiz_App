using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Models.Domain;
using QuizApp.Models.ViewModels;
using QuizApp.Services;

namespace QuizApp.Pages
{
    
    public class QuestionModel : PageModel
    {
        private IQuestionRepository _questionRepository;

        [BindProperty]
        public QuestionUpdateVM QuestionVM { get; set; }

        [BindProperty]
        public List<AnswerVM> AnswerVMs { get; set; }


        public QuestionModel(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
            AnswerVMs = new List<AnswerVM>();
        }
        public void OnGet(Guid questionId)
        {
            var question = _questionRepository.GetQuestionWithAnswers(questionId);
         
            if(question==null)
            {
                Console.WriteLine("Question not found");
            }
            else
            {
                QuestionVM = new QuestionUpdateVM
                {
                    Content = question.Content,
                    Id = question.Id,
                    CorrectAnswerId = question.CorrectAnswerId
                };

                foreach(var ans in question.Answers)
                {
                    AnswerVMs.Add(new AnswerVM
                    {
                        Id = ans.Id,
                        Content = ans.Content,
                    });
                }
            }
        }

        public IActionResult OnPostDelete()
        {
            //Get quiz Id for redirection
            var question = _questionRepository.GetQuestion(QuestionVM.Id);

            var quizId = question.QuizId;

            _questionRepository.Delete(question);
            _questionRepository.SaveChanges();

            
            return Redirect($"/Quiz/{quizId}");
        }
        public IActionResult OnPost()
        {
            
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return Redirect("/Error");
            }

            var question = _questionRepository.GetQuestionWithAnswers(QuestionVM.Id);

           
            try
            {
                //Update Question's properties from the VM
                question.Content = QuestionVM.Content;
                question.CorrectAnswerId = QuestionVM.CorrectAnswerId;

                //Update Answers
                foreach (var ans in question.Answers)
                {

                    ans.Content = AnswerVMs.Where(a => a.Id == ans.Id).FirstOrDefault().Content;
                }

                //Commit updates
                _questionRepository.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Failed in updating question with Id {QuestionVM.Id}: {e.Message}");
            }

          

            return Redirect($"/Quiz/{question.QuizId}");
        }
    }
}
