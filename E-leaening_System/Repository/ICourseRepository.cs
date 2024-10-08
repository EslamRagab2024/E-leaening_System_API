using E_leaening_System.Models;

namespace E_leaening_System.Repository
{
    public interface ICourseRepository: IRepository<Course>
    {
        public List<Course> GetAllIncludeInstructor();

    }
}

