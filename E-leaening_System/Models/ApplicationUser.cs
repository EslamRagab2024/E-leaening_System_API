using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_leaening_System.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string ?Address { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ?AccountType { get; set; }

        [ForeignKey("student")]
        public int? LearnerId { get; set; }
        public Student? student  { get; set; }

        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        [ForeignKey("Admin")]
        public int? adminid { get; set; }
        public Admin? Admin { get; set; }
    }
}