using System;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service.IService;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using MyPersonalPlannerBackend.Helpers;

namespace MyPersonalPlannerBackend.Service
{
    public class PlannerService : IPlannerService
    {
        private readonly IPlannerRepository _plannerRepository;
        private readonly IUserService _userService;


        public PlannerService(IPlannerRepository plannerRepository, IUserService userService)
        {
            _plannerRepository = plannerRepository;
            _userService = userService;
        }

        public IEnumerable<PlannerView> GetPlanners(int userId)
        {
            var plannerIds = GetPlannerIds(userId);

            var planners = new List<PlannerView>();

            foreach (var id in plannerIds)
            {
                var planner = _plannerRepository.GetPlanner(id);
                var items = GetPlannerItems(id);
                var users = GetUsersInPlanner(id);

                var plannerView = AssemblePlannerView(planner, items, users.WithoutPasswords());

                planners.Add(plannerView);
            }

            return planners;
        }

        public void CreatePlanner(int userId, string title)
        {
            var planner = new Planner()
            {
                Id = 0,
                Title = title,
                Owner = userId
            };
            _plannerRepository.AddPlanner(planner);
        }

        public void AddUserToPlanner(int loggedInUserId, UserPlanner model)
        {
            var user = _userService.GetUser(model.Username);
            var planner = _plannerRepository.GetPlanner(model.PlannerId);
            if (planner.Owner != loggedInUserId)
            {
                throw new AuthenticationException("You're not validated to add a user to this planner.");
            }
            
            var plannerUsers = GetUsersInPlanner(planner.Id);
            var alreadyInPlanner = plannerUsers.FirstOrDefault(plannerUser => plannerUser.Username == model.Username) != null;

            if (alreadyInPlanner)
            {
                throw new Exception("This user is already in this database");
            }
            _plannerRepository.AddUserToPlanner(model.PlannerId, user.Id);
        }
        
        public void RemoveUserFromPlanner(int loggedInUserId, UserPlanner model)
        {
            var user = _userService.GetUser(model.Username);
            var planner = _plannerRepository.GetPlanner(model.PlannerId);
            if (planner.Owner != loggedInUserId)
            {
                throw new AuthenticationException("You are not allowed to remove a user.");
            }
            _plannerRepository.RemoveUserFromPlanner(planner.Id, user.Id);
        }

        public void RemoveItemFromPlanner(User loggedInUser, int itemId)
        {
            var isInPlanner = CheckIfUserIsInPlannerByItemId(itemId, loggedInUser);
            if (isInPlanner)
            {
                PlannerItem plannerItem = _plannerRepository.GetPlannerItem(itemId);
                _plannerRepository.RemovePlannerItem(plannerItem);
            }
        }

        public void RemovePlanner(User user, int id)
        {
            Planner planner = _plannerRepository.GetPlanner(id);
            if (planner.Owner == user.Id)
            {
                _plannerRepository.RemovePlanner(planner);
            }
        }

        public void SetDonePlannerItem(User loggedInUser, int itemId, bool isDone)
        {
            var isInPlanner = CheckIfUserIsInPlannerByItemId(itemId, loggedInUser);
            if (isInPlanner)
            {
                _plannerRepository.UpdatePlannerItemIsDone(itemId, isDone);
            }
        }

        public void AddPlannerItem(int loggedInUserId, PlannerItem item)
        {
            var userIds = _plannerRepository.GetUserIdsInPlanner(item.PlannerId);
            if (!userIds.Contains(loggedInUserId))
            {
                throw new AuthenticationException("You're not validated to add an item to this planner.");
            }

            item.IsDone = false;

            _plannerRepository.AddPlannerItem(item);
        }

        private bool CheckIfUserIsInPlannerByItemId(int itemId, User loggedInUser)
        {
            PlannerItem plannerItem = _plannerRepository.GetPlannerItem(itemId);
            Planner planner = _plannerRepository.GetPlanner(plannerItem.PlannerId);
            var plannerUsers = GetUsersInPlanner(planner.Id);
            var isInPlanner = plannerUsers.FirstOrDefault(plannerUser => plannerUser.Username == loggedInUser.Username) != null;
            return isInPlanner;
        }

        private PlannerView AssemblePlannerView(Planner planner, IEnumerable<PlannerItem> items, IEnumerable<User> users)
        {
            var plannerItems = items.ToList();
            
            foreach (var item in plannerItems)
            {
                item.Planner = null;
            }

            var plannerView = new PlannerView()
            {
                Id = planner.Id,
                Owner = planner.Owner,
                Title = planner.Title,
                PlannerItems = plannerItems.ToList(),
                Users = users.ToList()
            };

            return plannerView;
        }

        private IEnumerable<int> GetPlannerIds(int userId)
        {
            var plannerIds = _plannerRepository.GetPlannerIds(userId);
            return plannerIds.Distinct();
        }

        private IEnumerable<PlannerItem> GetPlannerItems(int plannerId)
        {
            return _plannerRepository.GetPlannerItems(plannerId);
        }

        private IEnumerable<User> GetUsersInPlanner(int plannerId)
        {
            var userIds = _plannerRepository.GetUserIdsInPlanner(plannerId);
            var users = new List<User>();
            foreach (var id in userIds)
            {
                users.Add(_userService.GetUser(id));
            }

            return users;

        }
       
    }
}
