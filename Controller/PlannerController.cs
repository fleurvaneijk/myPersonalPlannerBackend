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
        public IActionResult AddUserToPlanner(UserPlanner model)
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

        [HttpDelete]
        public IActionResult DeletePlanner(int id)
        {
            var user = _userService.GetLoggedInUser(HttpContext);
            _plannerService.RemovePlanner(user, id);
            return Ok();
        }

        [HttpDelete("plannerItem")]
        public IActionResult DeletePlannerItem(int id)
        {
            var user = _userService.GetLoggedInUser(HttpContext);
            _plannerService.RemoveItemFromPlanner(user, id);
            return Ok();
        }
        
        [HttpPost("removeUserFromPlanner")]
        public IActionResult RemoveUserFromPlanner(UserPlanner model)
        {
            var loggedInUserId = _userService.GetLoggedInUser(HttpContext).Id;
            _plannerService.RemoveUserFromPlanner(loggedInUserId, model);
            return Ok();
        }
        
        [HttpPut("SetPlannerTitle")]
        public IActionResult SetPlannerTitle(int plannerId, string title)
        {
            var loggedInUser = _userService.GetLoggedInUser(HttpContext);
            _plannerService.SetPlannerTitle(loggedInUser, plannerId, title);
            return Ok();
        }

        [HttpPut("setDoneItem")]
        public IActionResult SetDonePlannerItem(int itemId, bool isDone)
        {
            var loggedInUser = _userService.GetLoggedInUser(HttpContext);
            _plannerService.SetDonePlannerItem(loggedInUser, itemId, isDone);
            return Ok();
        }
    }
}
