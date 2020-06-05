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

        public User GetUser(int id);

        User GetLoggedInUser(HttpContext context);
        
        void ChangeAgenda(User user, string agendaLink);
    }
}
