using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Models
{
    public class QuizAnswer
    {
        [Key]
        public int QuestionId { get; set; }
        public bool Answer { get; set; } 

        [ForeignKey("QuestionId")]
        public QuizQuestions? QuizQuestion { get; set; }
    }
}
