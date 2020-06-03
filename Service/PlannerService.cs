using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<int> GetPlannerIds(int userId)
        {
            var plannerIds = _plannerRepository.GetPlannerIds(userId);
            return plannerIds.Distinct();
        }

        public Planner GetPlanner(in int plannerId)
        {
            return _plannerRepository.GetPlanner(plannerId);
        }

        public IEnumerable<PlannerItem> GetPlannerItems(int plannerId)
        {
            return _plannerRepository.GetPlannerItems(plannerId);
        }

        public IEnumerable<User> GetUsersInPlanner(int plannerId)
        {
            var userIds = _plannerRepository.GetUserIdsInPlanner(plannerId);
            var users = new List<User>();
            foreach (var id in userIds)
            {
                users.Add(_userService.GetUserById(id));
            }

            return users;

        }
    }
}
