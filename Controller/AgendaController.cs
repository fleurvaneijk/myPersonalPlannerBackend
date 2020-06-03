using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult RetrieveAgendaFromGoogle()
        {
            var user = _userService.GetLoggedInUser(HttpContext);
            if (string.IsNullOrEmpty(user.AgendaLink)) return Ok("");
            Client.DefaultRequestHeaders.Accept.Clear();
            var request = Client.GetStreamAsync(user.AgendaLink);
            return Ok(request.Result);
        }


    }
}