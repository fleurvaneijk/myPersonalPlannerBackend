using DevOne.Security.Cryptography.BCrypt;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service.IService;
using System;

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

        public User GetUserByID(int id)
        {
            return  _userRepository.GetUserById(id);
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
    }
}
