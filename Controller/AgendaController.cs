using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Service.IService;

namespace MyPersonalPlannerBackend.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        
        private readonly IUserService _userService;
        
        private static readonly HttpClient Client = new HttpClient();

        public AgendaController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAgendaFromGoogle()
        {
            var userID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _userService.GetUserByID(Convert.ToInt32(userID));
            if (user.AgendaLink != "")
            {
                Client.DefaultRequestHeaders.Accept.Clear();
                var request = Client.GetStreamAsync(user.AgendaLink);
                return Ok(request.Result);
            }
            return Ok("");
        }


    }
}