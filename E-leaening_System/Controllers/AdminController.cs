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
    public class AdminController : ControllerBase
    {
        private readonly IRepository<Admin> Adminrepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AdminController(IRepository<Admin>Adminrepo,UserManager<ApplicationUser>userManager,IMapper mapper )
        {
            this.Adminrepo=Adminrepo;
            _userManager=userManager;
            _mapper=mapper;
        }
        [HttpPost]
        public async Task< IActionResult> AddAdmin(AdminDTO admindto)
        {
            if(ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(User);
                Admin admin=_mapper.Map<Admin>(admindto);
                admin.AccountId=currentuser.Id;
                Adminrepo.insert(admin);
                Adminrepo.save();
                return Ok("success add");
            }
            return BadRequest(" invalid data");
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            List<Admin> admins = Adminrepo.GetAll();
            if(admins!=null)
            {
                List<AdminDTO> list = new List<AdminDTO>();
                foreach (Admin item in admins)
                {
                    AdminDTO adminDTO = new AdminDTO();
                    adminDTO.Name = item.Name;
                    list.Add(adminDTO);
                }
                return Ok(list);
            }
            return NotFound("no admins");
        }

        [HttpGet("ByAdminId/{AdminId:int}")]
        [Authorize]
        public IActionResult GetAdminByAdminId(int AdminId)
        {
            var Admin = Adminrepo.Get(c => c.id == AdminId);
            if (Admin == null)
            {
                return NotFound("Admin not found");
            }
            AdminDTO adminstratorDTO = _mapper.Map<AdminDTO>(Admin);
            return Ok(adminstratorDTO);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAdminstrator(int id, AdminDTO adminDTO)
        {
            Admin admin = Adminrepo.Get(a => a.id == id);

            if (admin == null)
            {
                return NotFound("Adminstrator not found");
            }
            var currentUser = await _userManager.GetUserAsync(User);
            admin.Name = adminDTO.Name;
            admin.AccountId = currentUser.Id;
            Adminrepo.update(admin);
            Adminrepo.save();
            return Ok("Admin updated successfully");

        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteAdmin(int id)
        {
            Admin existingAdmin = Adminrepo.Get(a => a.id == id);
            if (existingAdmin == null)
            {
                return NotFound("Admin not found");
            }
            Adminrepo.delete(existingAdmin);
            Adminrepo.save();

            return Ok("Admin deleted successfully");
        }

    }
}
