using AutoMapper;
using E_leaening_System.Models;
using E_leaening_System.DTO;

namespace E_leaening_System
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseDTO>();

            CreateMap<InstructorDTO, Instructor>();
            CreateMap<Instructor, InstructorDTO>();


            CreateMap<QuizDTO, Quiz>();
            CreateMap<Quiz, QuizDTO>();

            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>();

            CreateMap<AdminDTO, Admin>();
            CreateMap<Admin, AdminDTO>();



            CreateMap<CertificateDTO, Certificate>();
            CreateMap<Certificate, CertificateDTO>();

            CreateMap<TheQuizzes, TheQuizzesDTO>();
            CreateMap<TheQuizzesDTO, TheQuizzes>();

            CreateMap<ContentDTO, Content>();
            CreateMap<Content, ContentDTO>();

        }
    }
}
