using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Service.IService
{
    public interface IUserService
    {
               
        User SignUp(User user);

        User ChangeUsername(ChangeUsername user);

        User ChangePassword(ChangePassword user);

        void DeleteAccount(User user);

        public User GetUserByID(int id);
        void ChangeAgenda(User user, string agendaLink);
    }
}
