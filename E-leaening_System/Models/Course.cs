﻿using E_leaening_System.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_leaening_System.Models
{
    public class Course:IDeletable
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?ImgPath { get; set; }
        public int DurationInHours { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Student>? students { get; set; }
        public ICollection<Content>? Contents { get; set; }
        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
