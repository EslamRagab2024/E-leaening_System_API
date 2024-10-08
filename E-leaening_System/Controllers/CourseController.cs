using AutoMapper;
using E_leaening_System.DTO;
using E_leaening_System.Models;
using E_leaening_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_leaening_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository courseRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        //seravia controller

        public CourseController(ICourseRepository courseRepo, UserManager<ApplicationUser> userManager,
            IMapper mapper)

        {
            this.courseRepo = courseRepo;
            this.userManager = userManager;
            this.mapper = mapper;
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            List<Course> courses = courseRepo.GetAllIncludeInstructor();
            if (courses != null)
            {
                List<CourseIncludeInstructorDTO> dTOs = new List<CourseIncludeInstructorDTO>();
                foreach (var course in courses) 
                {
                    CourseIncludeInstructorDTO dto=new CourseIncludeInstructorDTO();
                    dto.id = course.Id;
                    dto.Name = course.Name;
                    dto.ImgPath = course.ImgPath;
                    dto.DurationInHours = course.DurationInHours;
                    dto.InstructorName = course.Instructor.Name;

                    dTOs.Add(dto);
                }
                return Ok(dTOs);
            }
            return NotFound();

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCoure(CourseDTO courseDTO)
        {
            if(ModelState.IsValid)
            {
                var currentuser = await userManager.GetUserAsync(User);
                int insid = (int)currentuser.InstructorId;
                Course course=mapper.Map<Course>(courseDTO);
                course.InstructorId = insid;
                courseRepo.insert(course);
                courseRepo.save();
                return Ok("successfull");
            }
            return BadRequest();
        }


        [HttpGet("ByCourseId/{courseId:int}")]
        // [Authorize]
        public IActionResult GetCourseByCourseId(int courseId)
        {
            var course = courseRepo.Get(c => c.Id == courseId);
            if (course == null)
            {
                return NotFound(new
                {
                    Error = "Course Not Found"
                });
            }
            CourseDTO courseDTO = mapper.Map<CourseDTO>(course);

            return Ok(courseDTO);

        }
        [HttpGet("byInstructorId/{instructorId:int}")]
        [Authorize]
        public IActionResult GetCoursesForSpecificInstructor(int instructorId)
        {
            var courses=courseRepo.GetAll().Where(c=>c.InstructorId == instructorId).ToList();
            if(courses!=null)
            {
                List<CourseDTO> dTOs = new List<CourseDTO>();
                foreach (var item in courses) 
                {
                    CourseDTO dTO = mapper.Map<CourseDTO>(item);
                    dTOs.Add(dTO);  
                }
                return Ok(dTOs);
            }
            return NotFound();

        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCourse(int id, CourseDTO courseDTO)
        {
            var course=courseRepo.Get(c => c.Id == id);
            var currentuser = await userManager.GetUserAsync(User);
            int insid=(int) currentuser.InstructorId;
            if(course==null)
            {
                return NotFound();
            }
            course.Name = courseDTO.Name;
            course.InstructorId = insid;
            courseRepo.update(course);
            courseRepo.save();
            return Ok("successfull");
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteCourse(int id)
        {
            Course existingCourse = courseRepo.Get(c => c.Id == id);
            if (existingCourse == null)
            {
                return NotFound(new
                {
                    Error = "Course not found"
                });

            }
            courseRepo.delete(existingCourse);
            courseRepo.save();

            return Ok("successfull");
        }
    }
}
