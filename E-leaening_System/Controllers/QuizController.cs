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
    public class QuizController : ControllerBase
    {
        private readonly IRepository<Quiz> _quizRepository; 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Mapper _mapper;

        public QuizController(IRepository<Quiz> quizRepository, UserManager<ApplicationUser>
            userManager, Mapper mapper)
        {
            _quizRepository = quizRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            List<Quiz> quizzes = _quizRepository.GetAll();
            if (quizzes != null)
            {
                List<QuizDTO> dtos = new List<QuizDTO>();
                foreach (var quiz in quizzes)
                {
                    QuizDTO dto = new QuizDTO
                    {
                        Id = quiz.Id,
                        Mark = quiz.Mark,
                    };
                    dtos.Add(dto);
                }
                return Ok(dtos);
            }
            return NotFound("No quizzes found");
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetQuizById(int id)
        {
            var quiz = _quizRepository.Get(q => q.Id == id);
            if (quiz == null)
            {
                return NotFound("Quiz not found");
            }
            QuizDTO dto = _mapper.Map<QuizDTO>(quiz);
            return Ok(dto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddQuiz(QuizDTO quizDTO)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                int studentId = (int)currentUser.LearnerId;
                Quiz quiz = _mapper.Map<Quiz>(quizDTO);
                quiz.studentid = studentId;
                _quizRepository.insert(quiz);
                _quizRepository.save();
                return Ok("Quiz added successfully");
            }
            return BadRequest("Cannot add quiz");
        }



        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateQuiz(int id, QuizDTO quizDTO)
        {
            Quiz existingQuiz = _quizRepository.Get(q => q.Id == id);
            if (existingQuiz == null)
            {
                return NotFound("Quiz not found");
            }
            _mapper.Map(quizDTO, existingQuiz);
            _quizRepository.update(existingQuiz);
            _quizRepository.save();
            return Ok("Quiz updated successfully");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteQuiz(int id)
        {
            Quiz existingQuiz = _quizRepository.Get(q => q.Id == id);
            if (existingQuiz == null)
            {
                return NotFound("Quiz not found");
            }
            _quizRepository.delete(existingQuiz);
            _quizRepository.save();
            return Ok("Quiz deleted successfully");
        }
    }
}
