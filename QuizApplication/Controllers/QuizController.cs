using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Data;

namespace QuizApplication.Controllers
{
    public class QuizController : Controller
    {
       

        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Models.QuizQuestions> questions = _context.QuizQuestion.ToList();
            return View(questions);
        }
        [HttpPost]
        public IActionResult SubmitAnswers(Dictionary<int, string> answers)
        {
            var questions = _context.QuizQuestion.ToList();
            int score = 0;

            foreach (var question in questions)
            {
                var correctAnswer = _context.QuizAnswer
                    .FirstOrDefault(a => a.QuestionId == question.QuestionID);
                    
                if (correctAnswer != null && answers.ContainsKey(question.QuestionID))
                {
                    if ((correctAnswer.Answer == true && answers[question.QuestionID] == "True") ||
                        (correctAnswer.Answer == false && answers[question.QuestionID] == "False"))
                    {
                        score++;
                    }
                }
            }

            return RedirectToAction("Result", new { score });
        }

        public IActionResult Result(int score)
        {

            ViewBag.Score = score;
            return View();
        }

        public IActionResult Answer()
        {
            var correctAnswers = _context.QuizQuestion
                .Include(q => q.QuizAnswer)
                .ToList();
            return View(correctAnswers);
        }

    }
}
