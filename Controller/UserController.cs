﻿using System.Threading.Tasks;
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
        public async Task<IActionResult> ChangeUsername([FromBody]User model, string newUsername)
        {
            var user = _userService.ChangeUsername(model, newUsername);
            return Ok(user);
        }
    }
}
