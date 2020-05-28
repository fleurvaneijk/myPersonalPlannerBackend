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
        IAuthenticationService _authenticationService;

        public UserService(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public User SignUp(User user)
        {
            var salt = BCryptHelper.GenerateSalt(6);
            var hashedPassword = BCryptHelper.HashPassword(user.Password, salt);
            user.Password = hashedPassword;
            _userRepository.AddUser(user);
            return user;
        }

        public User ChangeUsername(ChangeUsername user)
        {
            var authenticatedUser = _authenticationService.Authenticate(user.Username, user.Password).Result;
            if(authenticatedUser != null)
            {
                authenticatedUser.Username = user.NewUsername;
                _userRepository.ChangeUsername(authenticatedUser);
                return authenticatedUser;
            } else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public User ChangePassword(ChangePassword user)
        {
            var authenticatedUser = _authenticationService.Authenticate(user.Username, user.Password).Result;
            if (authenticatedUser != null)
            {
                var salt = BCryptHelper.GenerateSalt(6);
                var hashedPassword = BCryptHelper.HashPassword(user.NewPassword, salt);
                authenticatedUser.Password = hashedPassword;
                _userRepository.ChangePassword(authenticatedUser);
                return authenticatedUser;
            } else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public User GetUserByID(int id)
        {
            return  _userRepository.GetUserByID(id);
        }



        public void DeleteAccount(User user)
        {
            var authenticatedUser = _authenticationService.Authenticate(user.Username, user.Password).Result;
            if (authenticatedUser != null)
            {
                _userRepository.DeleteUser(authenticatedUser);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }


        public void DeleteAccount(User user)
        {
            throw new NotImplementedException();
        }
    }
}
