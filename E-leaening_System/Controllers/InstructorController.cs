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
    public class InstructorController : ControllerBase
    {
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Mapper _mapper;

        public InstructorController(IRepository<Instructor> instructorRepository,
            UserManager<ApplicationUser> userManager, Mapper mapper)
        {
            _instructorRepository = instructorRepository;
            _userManager = userManager;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Instructor> instructors = _instructorRepository.GetAll();
            if (instructors != null)
            {
                List<InstructorDTO> instructorDTOs = instructors.Select(e => new InstructorDTO
                {
                    Name = e.Name,
                }).ToList();
                return Ok(instructorDTOs);
            }

            return NotFound("There is no Instructors");
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetInstructorById(int id)
        {

            Instructor instructor = _instructorRepository.Get(e => e.Id == id);
            if (instructor == null)
            {
                return NotFound("Instructor not found");
            }
            InstructorDTO instructorDTO = _mapper.Map<InstructorDTO>(instructor);
            return Ok(instructorDTO);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddInstructor(InstructorDTO instructorDTO)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                Instructor instructor = _mapper.Map<Instructor>(instructorDTO);
                instructor.AccountId = currentUser.Id;
                _instructorRepository.insert(instructor);
                _instructorRepository.save();
                return Ok("Instructor added successfully");


            }
            return BadRequest("Invalid instructor data");

        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateInstructor(int id, InstructorDTO instructorDTO)
        {
            Instructor instructor = _instructorRepository.Get(e => e.Id == id);

            if (instructor == null)
            {
                return NotFound("Instructor not found");
            }
            var currentUser = await _userManager.GetUserAsync(User);
            instructor.Name = instructorDTO.Name;
            instructor.AccountId = currentUser.Id;
            _instructorRepository.update(instructor);
            _instructorRepository.save();
            return Ok("Instructor updated successfully");

        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteInstructor(int id)
        {
            Instructor instructor = _instructorRepository.Get(e => e.Id == id);
            if (instructor == null)
            {
                return NotFound("Instructor not found");
            }
            _instructorRepository.delete(instructor);
            _instructorRepository.save();
            return Ok("Instructor deleted successfully");


        }
    }
}
