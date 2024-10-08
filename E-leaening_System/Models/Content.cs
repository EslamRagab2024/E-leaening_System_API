using E_leaening_System.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_leaening_System.Models
{
    public class Content:IDeletable
    {
        public int Id { get; set; }
        public string ?Type { get; set; }
        public string ?videoPathURL { get; set; }
        public string ?content { get; set; }
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        [ForeignKey("student")]
        public int? studentid { get; set; }
        public Student? student { get; set; }
        [ForeignKey("Admin")]
        public int? Adminid { get; set; }
        public Admin? Admin { get; set; }
    }
}
