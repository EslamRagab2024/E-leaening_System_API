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
    public class StudentController : ControllerBase
    {
        private readonly IRepository<Student> studentrepo ;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Mapper _mapper;

        public StudentController(IRepository<Student> studentrepo, UserManager<ApplicationUser> 
            userManager, Mapper mapper)
        {
            this.studentrepo = studentrepo;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Student> learners = studentrepo.GetAll();
            if (learners != null)
            {
                List<StudentDTO> dTOs = new List<StudentDTO>();
                foreach (var learner in learners)
                {
                    StudentDTO dTO = new StudentDTO();
                    dTO.Name = learner.Name;
                    dTOs.Add(dTO);
                }
                return Ok(dTOs);
            }
            return NotFound("There is no Learners");
        }
        [HttpGet("ByLearnerId/{learnerId:int}")]
        [Authorize]
        public IActionResult GetLearnerByLearnerId(int learnerId)
        {
            var learner = studentrepo.Get(c => c.Id == learnerId);
            if (learner == null)
            {
                return NotFound("Learner not found");
            }
            StudentDTO learnerDTO = _mapper.Map<StudentDTO>(learner);
            return Ok(learnerDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddLearner(StudentDTO learnerDTO)
        {
            if (ModelState.IsValid)
            {
                var currenUser = await _userManager.GetUserAsync(User);
                Student learner = _mapper.Map<Student>(learnerDTO);
                learner.AccountId = currenUser.Id;
                studentrepo.insert(learner);
                studentrepo.save();
                return Ok("Learner added successfuly");

            }
            return BadRequest("can not add");
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLearner(int id, StudentDTO learnerDTO)
        {
            Student existingLearner = studentrepo.Get(l => l.Id == id);
            var currenUser = await _userManager.GetUserAsync(User);
            if (existingLearner == null)
            {
                return NotFound("Learner not found");
            }
            var currentUser = await _userManager.GetUserAsync(User);
            existingLearner.Name = learnerDTO.Name;
            existingLearner.AccountId = currentUser.Id;
            studentrepo.update(existingLearner);
            studentrepo.save();

            return Ok("Learner updated successfully");

        }
        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteLearner(int id)
        {
            Student existingLearner = studentrepo.Get(l => l.Id == id);
            if (existingLearner == null)
            {
                return NotFound("Learner not found");
            }
            studentrepo.delete(existingLearner);
            studentrepo.save();

            return Ok("Learner deleted successfully");
        }
    }
}
