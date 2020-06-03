using DevOne.Security.Cryptography.BCrypt;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service.IService;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MyPersonalPlannerBackend.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

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
                _userRepository.UpdateUser(authenticatedUser);
                return authenticatedUser;
            } else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public User ChangePassword(ChangePassword user)
        {
            var authenticatedUser = _authenticationService.Authenticate(user.Username, user.Password).Result;
            if (authenticatedUser == null) throw new UnauthorizedAccessException();
            var salt = BCryptHelper.GenerateSalt(6);
            var hashedPassword = BCryptHelper.HashPassword(user.NewPassword, salt);
            authenticatedUser.Password = hashedPassword;
            _userRepository.UpdateUser(authenticatedUser);
            return authenticatedUser;

        }

        public User GetUserById(int id)
        {
            return  _userRepository.GetUserById(id);
        }

        public void ChangeAgenda(User user, string agendaLink)
        {
            user.AgendaLink = agendaLink;
            _userRepository.UpdateUser(user);
        }


        public void DeleteAccount(User user)
        {
            var authenticatedUser = _authenticationService.Authenticate(user.Username, user.Password).Result;
            if (authenticatedUser == null)
            {
                throw new UnauthorizedAccessException();
            }
            _userRepository.DeleteUser(authenticatedUser);
        }

        public User GetLoggedInUser(HttpContext context)
        {
            var userId = Convert.ToInt32(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = GetUserById(userId);
            return user;
        }
    }
}
