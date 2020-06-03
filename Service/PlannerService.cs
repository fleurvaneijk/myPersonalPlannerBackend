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

        public PlannerService(IPlannerRepository plannerRepository)
        {
            _plannerRepository = plannerRepository;
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
    }
}
