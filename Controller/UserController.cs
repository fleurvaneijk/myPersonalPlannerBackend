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
        
        [HttpPost("changeusername")]
        public IActionResult ChangeUsername([FromBody]ChangeUsername model)
        {
            var user = _userService.ChangeUsername(model);
            return Ok(user.WithoutPassword());
        }

        
        [HttpPost("changepassword")]
        public IActionResult ChangePassword([FromBody]ChangePassword model)
        {
            var user = _userService.ChangePassword(model);
            return Ok(user.WithoutPassword());
        }
        
        [HttpPost("changeAgenda")]
        public IActionResult ChangeAgenda([FromBody]Agenda agenda)
        {
            var user = _userService.GetLoggedInUser(HttpContext);
            _userService.ChangeAgendaForUser(user, agenda.AgendaLink);
            return Ok(user.WithoutPassword());
        }
        
        [HttpDelete("deleteaccount")]
        public IActionResult DeleteAccount([FromBody]User model)
        {
           _userService.DeleteUser(model);
            return Ok();
        }
    }
}
