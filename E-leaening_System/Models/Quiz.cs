using E_leaening_System.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_leaening_System.Models
{
    public class Quiz:IDeletable
    {
        public int Id { get; set; }
        public double Mark { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("student")]
        public int? studentid { get; set; }
        public Student? student { get; set; }
        
        
        [ForeignKey("TheQuizzes")]
        public int? quizzesid { get; set; }
        public TheQuizzes?  TheQuizzes{ get; set; }
    }
}