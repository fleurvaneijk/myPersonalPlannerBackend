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

        User ChangeUsername(ChangeUsername user);

        User ChangePassword(ChangePassword user);

        void DeleteAccount(User user);

        public User GetUserByID(int id);
    }
}
