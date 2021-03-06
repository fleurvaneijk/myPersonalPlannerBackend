using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service.IService;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyPersonalPlannerBackend.Helpers;

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
            user.Password = user.Password.HashPassword();
            _userRepository.AddUser(user);
            return user;
        }

        public User ChangeUsername(ChangeUsername user)
        {
            var authenticatedUser = _authenticationService.Authenticate(user.Username, user.Password).Result;
            
            authenticatedUser.Username = user.NewUsername;
            _userRepository.UpdateUser(authenticatedUser);
            return authenticatedUser;

        }

        public User ChangePassword(ChangePassword user)
        {
            var authenticatedUser = _authenticationService.Authenticate(user.Username, user.Password).Result;

            authenticatedUser.Password = user.NewPassword.HashPassword();
            _userRepository.UpdateUser(authenticatedUser);
            return authenticatedUser;

        }

        public User GetUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("A user with this ID doesn't exist.");
            }

            return user;
        }

        public User GetUser(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new Exception("A user with this username doesn't exist.");
            }

            return user;
        }

        public void ChangeAgendaForUser(User user, string agendaLink)
        {
            user.AgendaLink = agendaLink;
            _userRepository.UpdateUser(user);
        }


        public void DeleteUser(User user)
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
            var user = GetUser(userId);
            return user;
        }
    }
}
