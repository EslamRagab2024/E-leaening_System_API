using E_leaening_System.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_leaening_System.Models
{
    public class Certificate:IDeletable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("Quizzes")]
        public int? QuizId { get; set; }
        public TheQuizzes? Quizzes { get; set; }
        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}