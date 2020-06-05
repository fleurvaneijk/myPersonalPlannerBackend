using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service.IService;
using System.Collections.Generic;
using System.Linq;
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

                var plannerView = assemblePlannerView(planner, items, users);

                planners.Add(plannerView);
            }

            return planners;
        }

        private PlannerView assemblePlannerView(Planner planner, IEnumerable<PlannerItem> items, IEnumerable<User> users)
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

            return users.WithoutPasswords();

        }
       
    }
}
