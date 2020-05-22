using DevOne.Security.Cryptography.BCrypt;
using Microsoft.Extensions.Logging.Abstractions;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalPlannerBackend.Service
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User SignUp(User user)
        {
            var salt = BCryptHelper.GenerateSalt(6);
            var hashedPassword = BCryptHelper.HashPassword(user.Password, salt);
            user.Password = hashedPassword;
            _userRepository.AddUser(user);
            return user;
        }

        public void DeleteAccount(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
