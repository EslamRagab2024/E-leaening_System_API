using E_leaening_System.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_leaening_System.Models
{
    public class Instructor:IDeletable
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Account")]
        public string? AccountId { get; set; }
        public ApplicationUser? Account { get; set; }
        public ICollection<Course>? Coruses { get; set; }
        public ICollection<TheQuizzes>? TheQuizzes { get; set; }
    }
}
