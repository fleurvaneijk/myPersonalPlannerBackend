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
    }
}
