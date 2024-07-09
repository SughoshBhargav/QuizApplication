using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Models
{
    public class QuizQuestions
    {
        [Key]
        public int QuestionID { get; set; }
        public string? QuestionText { get; set; }
        public QuizAnswer? QuizAnswer { get; set; }
    }
}
