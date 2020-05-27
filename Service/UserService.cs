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
        AuthenticationService _authenticationService;

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

        public User ChangeUsername(User user, string newUsername)
        {
            if(_authenticationService.Authenticate(user.Username, user.Password) != null)
            {
                user.Username = newUsername;
                _userRepository.ChangeUsername(user);
                return user;
            } else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public User GetUserByID(int id)
        {
            return  _userRepository.GetUserByID(id);
        }

        public void DeleteAccount(string username, string password)
        public User ChangePassword(User user, string newPassword)
        {
            throw new NotImplementedException();
        }


        public void DeleteAccount(User user)
        {
            throw new NotImplementedException();
        }
    }
}
