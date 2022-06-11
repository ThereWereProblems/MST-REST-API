using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MST_REST_Web_API.Models.DTO;
using MST_REST_Web_API.Services;

namespace MST_REST_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterUrer([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            var IsDeleted = _accountService.Delete(id);
            if (IsDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);

            return Ok(token);
        }

        [HttpGet("getusers")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetUsers()
        {
            var listOfUsers = _accountService.GetAll();


            return Ok(listOfUsers);
        }

        [HttpPatch("changepassword")]
        [Authorize]
        public ActionResult ChangePassword([FromBody] NewPasswordDto dto)
        {
            _accountService.ChangePassword(dto);
            return Ok();
        }

        [HttpPatch("edituser")]
        [Authorize]
        public ActionResult EditUser([FromBody] UserEditDto dto)
        {
            _accountService.EditUser(dto);
            return Ok();
        }

        [HttpPatch("changerole/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangeRole([FromRoute] int id, [FromBody] ChangeRoleDto dto)
        {
            _accountService.ChangeRole(id, dto);
            return Ok();
        }
    }
}
