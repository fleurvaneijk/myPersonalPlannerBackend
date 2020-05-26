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

        void DeleteAccount(string username, string password);

        public User GetUserByID(int id);
    }
}
