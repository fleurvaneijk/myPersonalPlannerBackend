using Microsoft.AspNetCore.Http;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Service.IService
{
    public interface IUserService
    {
               
        User SignUp(User user);

        User ChangeUsername(ChangeUsername user);

        User ChangePassword(ChangePassword user);

        void DeleteUser(User user);

        User GetUser(int id);
        User GetUser(string username);

        User GetLoggedInUser(HttpContext context);
        
        void ChangeAgendaForUser(User user, string agendaLink);
    }
}
