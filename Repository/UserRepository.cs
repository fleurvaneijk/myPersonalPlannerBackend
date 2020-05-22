using System.Linq;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;

namespace MyPersonalPlannerBackend.Repository
{
    public class UserRepository : IUserRepository
    {
        private MariaDBContext _context;

        public UserRepository(MariaDBContext context)
        {
            _context = context;
        }

              public User GetUser(string username)
        {
            return _context.Users
                .SingleOrDefault(u => u.Username == username);
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void DeleteUser(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
