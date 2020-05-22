using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Repository.IRepository
{
    public interface IUserRepository
    {
        User GetUser(string username);

        User AddUser(User user);

        void DeleteUser(int id);
    }
}
