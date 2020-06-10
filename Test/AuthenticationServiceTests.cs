using System;
using Moq;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service;
using MyPersonalPlannerBackend.Service.IService;
using NUnit.Framework;

namespace MyPersonalPlannerBackend.Test
{
    public class AuthenticationServiceTests
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        
        public AuthenticationServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authenticationService = new AuthenticationService(_userRepositoryMock.Object);
        }
        
        [SetUp]
        public void Setup()
        {
            _userRepositoryMock.Setup(ur => ur.GetUserByUsername(It.IsAny<String>()))
                .Returns(GetUser());
        }
        
        [Test]
        public void Authenticate_PasswordIsCorrect_UserShouldBeAuthenticated()
        {
            var username = "fleur";
            var password = "kaas";
            var result = _authenticationService.Authenticate(username, password);
            Assert.AreEqual(result.Result.Username, "fleur");
        }
        
        [Test]
        public void Authenticate_PasswordIsNotCorrect_UserShouldNotBeAuthenticated()
        {
            var username = "fleur";
            var password = "kaas1";
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                await _authenticationService.Authenticate(username, password);
            });
        }
        
        [Test]
        public void Authenticate_UsernameIsNotCorrect_UserShouldNotBeAuthenticated()
        {
            var username = "fleur1";
            var password = "kaas";
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                await _authenticationService.Authenticate(username, password);
            });
        }
        
        private User GetUser()
        {
            return new User()
            {
                Id = 1,
                Username = "fleur",
                Password = "$2a$06$TBBvjvYkn28pm/pFPtrgaOU5RCsx7fKOg09fbwH.IxK3fVN8gvsP6"
            };
        }
        
    }
}