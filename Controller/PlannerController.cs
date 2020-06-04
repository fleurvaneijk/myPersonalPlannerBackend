using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPersonalPlannerBackend.Helpers;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Service.IService;
using PlannerItem = MyPersonalPlannerBackend.Model.PlannerItem;

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

        [HttpGet("getPlannerIds")]
        public IEnumerable<int> GetPlannerIds()
        {
            var userId = _userService.GetLoggedInUser(HttpContext).Id;
            var plannerIds = _plannerService.GetPlannerIds(userId);

            return plannerIds;
        }

        [HttpGet("getPlanner")]
        public Planner GetPlanner(int plannerId)
        {
            return _plannerService.GetPlanner(plannerId);
        }

        [HttpGet("getPlannerItems")]
        public IEnumerable<PlannerItem> GetPlannerItems(int plannerId)
        {
            return _plannerService.GetPlannerItems(plannerId);
        }

        [HttpGet("getUsersInPlanner")]
        public IEnumerable<User> GetUsersInPlanner(int plannerId)
        {
            return _plannerService.GetUsersInPlanner(plannerId).WithoutPasswords();
        }

    }
}
