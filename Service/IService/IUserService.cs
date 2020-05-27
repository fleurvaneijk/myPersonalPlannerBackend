using MyPersonalPlannerBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalPlannerBackend.Service.IService
{
    public interface IUserService
    {
               
        User SignUp(User user);

        User ChangeUsername(User user, string newUsername);

        User ChangePassword(User user, string newPassword);

        void DeleteAccount(User user);

        public User GetUserByID(int id);
    }
}
