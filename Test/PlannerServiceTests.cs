using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Moq;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service;
using MyPersonalPlannerBackend.Service.IService;
using NUnit.Framework;

namespace MyPersonalPlannerBackend.Test
{
    public class PlannerServiceTests
    {
        private readonly IPlannerService _plannerService;
        private readonly Mock<IPlannerRepository> _plannerRepositoryMock;
        private readonly Mock<IUserService> _userServiceMock;

        public PlannerServiceTests()
        {
            _plannerRepositoryMock = new Mock<IPlannerRepository>();
            _userServiceMock = new Mock<IUserService>();
            _plannerService = new PlannerService(_plannerRepositoryMock.Object, _userServiceMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            _plannerRepositoryMock.Setup(pr => pr.GetPlannerIds(It.IsAny<int>()))
                .Returns(new List<int>() { 1 });
            _plannerRepositoryMock.Setup(pr => pr.GetPlanner(It.IsAny<int>()))
                .Returns(GetPlanner());
            _plannerRepositoryMock.Setup(pr => pr.GetPlannerItems(It.IsAny<int>()))
                .Returns(GetPlannerItems());
            _plannerRepositoryMock.Setup(pr => pr.GetUserIdsInPlanner(It.IsAny<int>()))
                .Returns(new List<int>() { 1 });
            _plannerRepositoryMock.Setup(pr => pr.GetPlannerItem(It.IsAny<int>()))
                .Returns(GetPlannerItems()[0]);

            _userServiceMock.Setup(us => us.GetUser(It.IsAny<int>()))
                .Returns(GetUser());
            _userServiceMock.Setup(us => us.GetUser(It.IsAny<string>()))
                .Returns(GetUser());
        }

        [Test]
        public void GetPlanners_UserPasswordsShouldBeNull()
        {
            var planners = _plannerService.GetPlanners(1).ToList();
            var userPassword = planners[0].Users[0].Password;
            Assert.IsNull(userPassword);
        }

        [Test]
        public void CreatePlanner_RepositoryShouldGetCalled()
        {
            var loggedInUserId = 1;
            var title = "fleur's planner";

            _plannerService.CreatePlanner(loggedInUserId, title);

            _plannerRepositoryMock.Verify(x =>
                    x.AddPlanner(It.IsAny<Planner>()),
                    Times.Once());
        }

        [Test]
        public void AddUserToPlanner_LoggedInUserIsNotOwner_IsNotValidatedToAddUser()
        {
            var loggedInUserId = 2;

            var ex = Assert.Throws<AuthenticationException>(
                () => _plannerService.AddUserToPlanner(loggedInUserId, GetUserPlanner()));

            Assert.That(ex.Message, Is.EqualTo("You're not validated to add a user to this planner."));
        }

        [Test]
        public void AddUserToPlanner_UserIsAlreadyInPlanner_ThrowsException()
        {
            var loggedInUserId = 1;

            var ex = Assert.Throws<Exception>(
                () => _plannerService.AddUserToPlanner(loggedInUserId, GetUserPlanner()));

            Assert.That(ex.Message, Is.EqualTo("This user is already in this database"));
        }

        [Test]
        public void RemoveUserFromPlanner_LoggedInUserIsNotOwner_IsNotValidatedToAddUser()
        {
            var loggedInUserId = 2;

            var ex = Assert.Throws<AuthenticationException>(
                () => _plannerService.RemoveUserFromPlanner(loggedInUserId, GetUserPlanner()));

            Assert.That(ex.Message, Is.EqualTo("You are not allowed to remove a user."));
        }

        [Test]
        public void RemoveUserFromPlanner_LoggedInUserIsOwner_OwnerCannotDeleteThemseleves()
        {
            var loggedInUserId = 1;

            var ex = Assert.Throws<Exception>(
                () => _plannerService.RemoveUserFromPlanner(loggedInUserId, GetUserPlanner()));

            Assert.That(ex.Message, Is.EqualTo("The owner cannot remove themselves from the planner."));
        }

        [Test]
        public void RemoveItemFromPlanner_ItemIsInPlanner_RepositoryShouldGetCalled()
        {
            var loggedInUser = GetUser();

            _plannerService.RemoveItemFromPlanner(loggedInUser, GetPlannerItems()[0].Id);

            _plannerRepositoryMock.Verify(x =>
                    x.RemovePlannerItem(It.IsAny<PlannerItem>()),
                    Times.Once());
        }

        [Test]
        public void RemovePlanner_LoggedInUserIsOwner_RepositoryShouldGetCalled()
        {
            var loggedInUser = GetUser();

            _plannerService.RemovePlanner(loggedInUser, GetPlanner().Id);

            _plannerRepositoryMock.Verify(x =>
                    x.RemovePlanner(It.IsAny<Planner>()),
                Times.Once());
        }

        [Test]
        public void AddPlannerItem_LoggedInUserIsNotOwner_IsNotValidatedToAddItem()
        {
            var loggedInUserId = 2;

            var ex = Assert.Throws<AuthenticationException>(
                () => _plannerService.AddPlannerItem(loggedInUserId, GetPlannerItems()[0]));

            Assert.That(ex.Message, Is.EqualTo("You're not validated to add an item to this planner."));
        }

        [Test]
        public void AddPlannerItem_LoggedInUserIsOwner_RepositoryShouldGetCalled()
        {
            var loggedInUserId = 1;

            _plannerService.AddPlannerItem(loggedInUserId, GetPlannerItems()[0]);

            _plannerRepositoryMock.Verify(x =>
                    x.AddPlannerItem(It.IsAny<PlannerItem>()),
                    Times.Once());
        }

        [Test]
        public void SetDonePlannerItem_LoggedInUserIsOwner_RepositoryShouldGetCalled()
        {
            var loggedInUser = GetUser();
            var itemId = 1;

            _plannerService.SetDonePlannerItem(loggedInUser, itemId, true);

            _plannerRepositoryMock.Verify(x => 
                x.UpdatePlannerItemIsDone(It.IsAny<int>(), It.IsAny<bool>()), 
                Times.Once());
        }

        private Planner GetPlanner()
        {
            return new Planner()
            {
                Id = 1,
                Title = "MockPlannerTitle",
                Owner = 1,
                PlannerItems = GetPlannerItems()
            };
        }

        private IList<PlannerItem> GetPlannerItems()
        {
            return new List<PlannerItem>()
            {
                new PlannerItem()
                {
                    Id = 1,
                    PlannerId = 1,
                    User = 2,
                    Day = 0,
                    Title = "PlannerItem1Title",
                    IsDone = false
                },
                new PlannerItem()
                {
                    Id = 2,
                    PlannerId = 1,
                    User = 3,
                    Day = 1,
                    Title = "PlannerItem2Title",
                    IsDone = true
                }
            };
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

        private UserPlanner GetUserPlanner()
        {
            return new UserPlanner()
            {
                PlannerId = 1,
                Username = "fleur"
            };
        }
    }
}