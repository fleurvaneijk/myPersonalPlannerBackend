using System;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service.IService;

namespace MyPersonalPlannerBackend.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(( ) => _userRepository.GetUserByUsername(username));
            if (user != null && user.Username == username && BCryptHelper.CheckPassword(password, user.Password))
            {
                return user;
            }
            throw new UnauthorizedAccessException("");
        }
    }
}
