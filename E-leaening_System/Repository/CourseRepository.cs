using E_leaening_System.Models;
using Microsoft.EntityFrameworkCore;

namespace E_leaening_System.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {

        public CourseRepository(MyData context):base(context) { }

        public List<Course> GetAllIncludeInstructor()
        {
            return context.Courses
                .Include(course => course.Instructor)
                .Where(item => item.IsDeleted == false).ToList();
        }

    }
}

