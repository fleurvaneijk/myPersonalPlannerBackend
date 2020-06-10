using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Repository.IRepository
{
    public interface IUserRepository
    {
        User AddUser(User user);

        User UpdateUser(User user);

        void DeleteUser(User user);
        User GetUserById(int id);
        User GetUserByUsername(string username);
    }
}
