using System;
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
        public async Task<IActionResult> SignUp([FromBody]User model)
        {
            var user = _userService.SignUp(model);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("changeusername")]
        public async Task<IActionResult> ChangeUsername([FromBody]ChangeUsername model)
        {
            var user = _userService.ChangeUsername(model);
            return Ok(user.WithoutPassword());
        }


        [AllowAnonymous]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePassword model)
        {
            var user = _userService.ChangePassword(model);
            return Ok(user.WithoutPassword());
        }

        [AllowAnonymous]
        [HttpDelete("deleteaccount")]
        public async Task<IActionResult> DeleteAccount([FromBody]User model)
        {
           _userService.DeleteAccount(model);
            return Ok();
        }
    }
}
