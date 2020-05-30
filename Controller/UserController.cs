using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPersonalPlannerBackend.Helpers;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Service.IService;

namespace MyPersonalPlannerBackend.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody]User model)
        {
            var user = _userService.SignUp(model);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("changeusername")]
        public IActionResult ChangeUsername([FromBody]ChangeUsername model)
        {
            var user = _userService.ChangeUsername(model);
            return Ok(user.WithoutPassword());
        }


        [AllowAnonymous]
        [HttpPost("changepassword")]
        public IActionResult ChangePassword([FromBody]ChangePassword model)
        {
            var user = _userService.ChangePassword(model);
            return Ok(user.WithoutPassword());
        }
        
        [HttpPost("changeAgenda")]
        public IActionResult ChangeAgenda([FromBody]Agenda agenda)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserByID(Convert.ToInt32(userId));
            _userService.ChangeAgenda(user, agenda.AgendaLink);
            return Ok(user.WithoutPassword());
        }

        [AllowAnonymous]
        [HttpDelete("deleteaccount")]
        public IActionResult DeleteAccount([FromBody]User model)
        {
           _userService.DeleteAccount(model);
            return Ok();
        }
    }
}
