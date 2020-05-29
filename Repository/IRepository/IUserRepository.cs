using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Repository.IRepository
{
    public interface IUserRepository
    {
        User GetUser(string username);

        User AddUser(User user);

        User ChangeUsername(User user);

        User ChangePassword(User user);

        void DeleteUser(User user);
        User GetUserById(int id);
    }
}
