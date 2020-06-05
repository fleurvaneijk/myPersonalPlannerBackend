using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Service.IService;

namespace MyPersonalPlannerBackend.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlannerController : ControllerBase
    {
        private readonly IPlannerService _plannerService;
        private readonly IUserService _userService;

        public PlannerController(IPlannerService plannerService, IUserService userService)
        {
            _plannerService = plannerService;
            _userService = userService;
        }

        [HttpGet("getPlanners")]
        public IEnumerable<PlannerView> GetPlanners()
        {
            var userId = _userService.GetLoggedInUser(HttpContext).Id;
            var planners = _plannerService.GetPlanners(userId);
            return planners;
        }

        [HttpPost("createPlanner")]
        public IActionResult CreatePlanner(string title)
        {
            var userId = _userService.GetLoggedInUser(HttpContext).Id;
            _plannerService.CreatePlanner(userId, title);
            return Ok();
        }

        [HttpPost("addUserToPlanner")]
        public IActionResult AddUserToPlanner(AddUserToPlanner model)
        {
            var loggedInUserId = _userService.GetLoggedInUser(HttpContext).Id;
            _plannerService.AddUserToPlanner(loggedInUserId, model);
            return Ok();
        }

        [HttpPost("addPlannerItem")]
        public IActionResult AddPlannerItem([FromBody] PlannerItem item)
        {
            var loggedInUserId = _userService.GetLoggedInUser(HttpContext).Id;
            _plannerService.AddPlannerItem(loggedInUserId, item);
            return Ok();
        }
    }
}
