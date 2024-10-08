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
    public class CertificateController : ControllerBase
    {
        private readonly IRepository<Certificate> certificateRepo;
        private readonly IRepository<Quiz> quizRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
       

        public CertificateController(IRepository<Certificate> certificateRepo, UserManager<ApplicationUser>
            userManager, IRepository<Quiz> quizRepo,IMapper mapper)
        {
            this.certificateRepo = certificateRepo;
            this.quizRepo = quizRepo;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        [HttpGet]
        //[Authorize]
        public IActionResult GetAll()
        {
            List<Certificate> certificates =certificateRepo.GetAll();
            if(certificates!=null)
            {
                List<CertificateDTO> result=certificates.Select(e=> new CertificateDTO
                {
                    Date = e.Date,
                }) .ToList();
                return Ok(result);
            }
            return NotFound("There is no Certificates");
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetCertficateById(int id)
        {
            Certificate certificate = certificateRepo.Get(e => e.Id == id);
            if (certificate == null)
            {
                return NotFound("Certtificate not found");
            }
            CertificateDTO certificateDTO = mapper.Map<CertificateDTO>(certificate);
            return Ok(certificateDTO);

        }


        [HttpPost]
        [Authorize]
        public async  Task <ActionResult> AddCertificate(CertificateDTO certificateDTO)
        {
            if(ModelState.IsValid) 
            {
                var currentuser =await userManager.GetUserAsync(User);
                Certificate certificate=mapper.Map<Certificate>(certificateDTO);
                certificate.Date= DateTime.UtcNow;// data & time on this computer
                certificateRepo.insert(certificate);
                certificateRepo.save();
                return Ok("Certificate added successfully");
            }
            return BadRequest("invalid data ");
        }

        [HttpPut("id")]
        [Authorize]
        public ActionResult UpdateCertificate(int id , CertificateDTO certificateDTO)
        {
            if(ModelState.IsValid) 
            {
                Certificate certificate=certificateRepo.Get(c=>c.Id==id);
                if (certificate == null)
                    return NotFound();
                certificate.Date = certificateDTO.Date;
                certificateRepo.update(certificate);
                certificateRepo.save();
                return Ok("Certificate updated successfully");
            }
            return BadRequest("invalid");
        }

        [HttpDelete("id")]
        [Authorize]
        public ActionResult DeleteCertificate(int id) 
        {
            Certificate certificate = certificateRepo.Get(e => e.Id == id);
            if (certificate == null)
            {
                return NotFound("Certtificate not found");
            }
            certificateRepo.delete(certificate);
            certificateRepo.save();
            return Ok("Certificate deleted successfully");
        }

        [HttpPost("print")]
        [Authorize]
        public async Task<IActionResult> PrintCertificateForCourse(int theQuizzesId)
        {
            var quizzesforthequizzes=quizRepo.GetAll().Where(e=>e.quizzesid==theQuizzesId);
            if (quizzesforthequizzes == null || !quizzesforthequizzes.Any())
            {
                return NotFound("No quizzes found for the specified TheQuizzes entity");
            }
            var currentuser=await userManager.GetUserAsync(User);
            // Check if the learner has passed all quizzes associated with the specified TheQuizzes entity
            var passedall= CheckIfLearnerPassedAllQuizzes(quizzesforthequizzes);
            if(passedall)
            {
                var certificate = GenerateCertificateForTheQuizzes(currentuser);
                certificateRepo.insert(certificate);
                certificateRepo.save();
                return Ok("Certificate issued successfully");

            }
            return BadRequest("Learner has not passed all quizzes ");
        }

        private bool CheckIfLearnerPassedAllQuizzes(IEnumerable<Quiz> quizzes)
        {
            foreach (var item in quizzes)
            {
                if(!CheckIfLearnerPassedQuiz(item))
                {
                    return false; // fail 
                }
            }
            return true;// passed all
        }
        private bool CheckIfLearnerPassedQuiz(Quiz quiz)
        {
            return quiz.Mark > 60;
        }
        private Certificate GenerateCertificateForTheQuizzes(ApplicationUser currentUser)
        {
            var certificate = new Certificate
            {
                Date = DateTime.UtcNow,//add another info
            };
            return certificate;
        }

    }
}
