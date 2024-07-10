using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Data;
using QuizApplication.Models;

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
            TempData["Success"] = "Quiz submitted Successfully";
            return RedirectToAction("Result", new { score });
        }

        public IActionResult Result(int score)
        {

            ViewBag.Score = score;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(QuizQuestions obj)
        {
            if (ModelState.IsValid)
            {
                _context.QuizQuestion.Add(obj);
                

                if (obj.QuizAnswer != null)
                {
                    obj.QuizAnswer.QuestionId = obj.QuestionID;
                    _context.QuizAnswer.Add(obj.QuizAnswer);
                }
                _context.SaveChanges();
                TempData["Success"] = "Question added Successfully";
            }
            return RedirectToAction("Create", "Quiz");
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
