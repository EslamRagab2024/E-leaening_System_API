using E_leaening_System.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_leaening_System.Models
{
    public class Admin:IDeletable
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Account")]
        public string? AccountId { get; set; }
        public ApplicationUser? Account { get; set; }
        [ForeignKey("Content")]
        public int? ContentId { get; set; }
        public Content? Content { get; set; }
    }
}
