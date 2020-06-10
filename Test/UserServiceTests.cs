using Moq;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service;
using MyPersonalPlannerBackend.Service.IService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalPlannerBackend.Test
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAuthenticationService> _authenticationServiceMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _userService = new UserService(_userRepositoryMock.Object, _authenticationServiceMock.Object);
        }


        [SetUp]
        public void Setup()
        {
            _userRepositoryMock.Setup(ur => ur.GetUserByUsername(It.IsAny<string>()))
                .Returns(GetUser());
            _userRepositoryMock.Setup(ur => ur.GetUserById(It.IsAny<int>()))
                .Returns(GetUser());
            _userRepositoryMock.Setup(ur => ur.UpdateUser(It.IsAny<User>()))
                .Returns(GetUser());
            _authenticationServiceMock.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(GetTask());
        }

        [Test]
        public void SignUp_PasswordShouldBeHashed()
        {
            var user = GetUser();
            user.Password = "test";
            
            var signedUpUser = _userService.SignUp(user);
            Assert.AreNotEqual("test", signedUpUser.Password);

        }

        [Test]
        public void SignUp_RepositoryShouldGetCalled()
        {
            
            //setup
            _userRepositoryMock.Setup(ur => ur.AddUser(It.IsAny<User>()))
                .Returns(GetUser());


            _userService.SignUp(GetUser());

            _userRepositoryMock.Verify(x =>
                    x.AddUser(It.IsAny<User>()),
                Times.AtLeast(1));
        }

        [Test]
        public void ChangeUsername_UsernameIsChanged()
        {
            var user = GetChangeUsername();

            var newUser = _userService.ChangeUsername(user);
            Assert.AreEqual(user.NewUsername, newUser.Username);
        }

        [Test]
        public void ChangeUsername_RepositoryShouldGetCalled()
        {
            _userService.ChangeUsername(GetChangeUsername());

            _userRepositoryMock.Verify(x =>
                x.UpdateUser(It.IsAny<User>()),
                Times.AtLeast(1));
        }

        [Test]
        public void ChangePassword_PasswordIsChanged()
        {
            var user = GetChangePassword();

            var newUser = _userService.ChangePassword(user);
            Assert.AreNotEqual(user.Password, newUser.Password);
        }

        [Test]
        public void ChangePassword_RepositoryShouldGetCalled()
        {
            _userService.ChangePassword(GetChangePassword());

            _userRepositoryMock.Verify(x =>
                x.UpdateUser(It.IsAny<User>()),
                Times.AtLeast(1));
        }

        [Test]
        public void GetUserWithId_ShouldReturnUser()
        {
            var checkUser = GetUser();
            var user = _userService.GetUser(1);
            Assert.AreEqual(checkUser.Username, user.Username);
        }

        [Test]
        public void GetUserWithUsername_ShouldReturnUser()
        {
            var checkUser = GetUser();
            var user = _userService.GetUser("fluhr");
            Assert.AreEqual(checkUser.Username, user.Username);
        }

        [Test]
        public void ChangeAgendaForUser_AgendaShouldGetChanged()
        {
            var user = GetUser();
            string agendaLink = "agenda link";

            var checkUser = GetUser();

            _userService.ChangeAgendaForUser(user, agendaLink);
            Assert.AreNotEqual(checkUser.AgendaLink, user.AgendaLink);
        }

        [Test]
        public void ChangeAgendaForUser_RepositoryShouldGetCalled()
        {
            _userService.ChangeAgendaForUser(GetUser(), "agenda link");

            _userRepositoryMock.Verify(x =>
                    x.UpdateUser(It.IsAny<User>()),
                Times.AtLeast(1));
        }

        [Test]
        public void DeleteUser_RepositoryShouldGetCalled()
        {
            _userService.DeleteUser(GetUser());

            _userRepositoryMock.Verify(x =>
                x.DeleteUser(It.IsAny<User>()),
                Times.Once);
        }
        
        //helper methods
        private User GetUser()
        {
            return new User()
            {
                Id = 1,
                Username = "fluhr",
                Password = "$2a$06$TBBvjvYkn28pm/pFPtrgaOU5RCsx7fKOg09fbwH.IxK3fVN8gvsP6"
            };
        }

        private ChangeUsername GetChangeUsername()
        {
            return new ChangeUsername()
            {
                Username = "Simon",
                Password = "$2a$06$TBBvjvYkn28pm/pFPtrgaOU5RCsx7fKOg09fbwH.IxK3fVN8gvsP6",
                NewUsername = "fluhr"
            };
        }

        private ChangePassword GetChangePassword()
        {
            return new ChangePassword()
            {
                Username = "Simon",
                Password = "$2a$06$TBBvjvYkn28pm/pFPtrgaOU5RCsx7fKOg09fbwH.IxK3fVN8gvsP6",
                NewPassword = "$2a$06$/K.SCcAI5VjZN93isDoK7OcBPUI1pzyxx4VtQAvIFNuyQ6qLE/mCC"
            };
        }

        private Task<User> GetTask()
        {
            return Task.Run(() => GetUser());
        }
    }
}
