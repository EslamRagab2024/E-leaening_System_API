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
    public class ContentController : ControllerBase
    {
        private readonly IRepository<Content> contentRepo;
        private readonly ICourseRepository courseRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ContentController(IRepository<Content> contentRepo, ICourseRepository courseRepo,
            UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.contentRepo = contentRepo;
            this.courseRepo = courseRepo;
            this.userManager = userManager;
            this.mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            List<Content> contents = contentRepo.GetAll();
            if (contents != null) 
            {
                List < ContentDTO > dto= contents.Select(c => new ContentDTO
                {
                    Id = c.Id,
                    Type = c.Type,
                    content = c.Type,
                    videoPathURL = c.videoPathURL,
                }).ToList();
                return Ok(dto);
            }
            return NotFound("no content");
        }

        [HttpGet("id")]
        public IActionResult GetContentById(int id)
        {
            var content = contentRepo.Get(c => c.Id == id);
            if (content == null)
            {
                return NotFound("Content not found");
            }
            ContentDTO dto = new ContentDTO
            {
                Id = content.Id,
                Type = content.Type,
                content = content.content,
                videoPathURL = content.videoPathURL,
            };
            return Ok(dto);
        }


        [HttpPost]
        [Authorize]
        public IActionResult PostContent([FromBody] ContentDTO contentDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Content content = new Content
                    {
                        Type = contentDto.Type,
                    };
                    contentRepo.insert(content);
                    contentRepo.save();

                    return Ok("Content added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "An error occurred while adding content");
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize]
        public  IActionResult UpdateContent(int id, ContentDTO contentDTO)
        {
            if(ModelState.IsValid) 
            {
                try
                {
                   var existingcontent=contentRepo.Get(c => c.Id == id);
                   if(existingcontent == null) 
                   {
                        return NotFound("content not found");
                   }
                   existingcontent.Type = contentDTO.Type;
                    contentRepo.update(existingcontent);
                    contentRepo.save();
                    return Ok("update successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "An error occurred while updating content");
                }
            }

            return BadRequest("");
        }

        [HttpDelete("id")]
        [Authorize]
        public IActionResult DeleteContent(int id) 
        {
            try
            {
                Content existingContent = contentRepo.Get(c => c.Id == id);
                if (existingContent == null)
                {
                    return NotFound("Content not found");
                }
                contentRepo.delete(existingContent);
                contentRepo.save();

                return Ok("Content deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting content");
            }
        }

        [HttpGet("course/{courseId}")]
        public IActionResult GetContentsByCourseId(int courseId)
        {
            try
            {
                var course=courseRepo.Get(c=>c.Id == courseId);
                if(course == null) { return NotFound(); }
                List<ContentDTO> contentDTOs=contentRepo.GetAll().Where(c=>c.Id==courseId)
                    .Select(c=> mapper.Map<ContentDTO>(c)).ToList();
                return Ok(contentDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving contents by course id");
            }
        }
    }
}
