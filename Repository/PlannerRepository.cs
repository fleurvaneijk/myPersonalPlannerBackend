using System;
using System.Collections.Generic;
using System.Linq;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository.IRepository;

namespace MyPersonalPlannerBackend.Repository
{
    public class PlannerRepository : IPlannerRepository
    {
        private readonly MariaDBContext _context;

        public PlannerRepository(MariaDBContext context)
        {
            _context = context;
        }

        public IEnumerable<int> GetPlannerIds(int userId)
        {
            var plannerIds = _context.PlannerUsers
                .Where(pu => pu.UserId ==  userId)
                .Select(i => i.PlannerId)
                .ToList();


            return plannerIds;
        }

        public Planner GetPlanner(int plannerId)
        {
            return  _context.Planners.Find(plannerId);
        }

        public IEnumerable<PlannerItem> GetPlannerItems(int plannerId)
        {
            return _context.PlannerItems
                .Where(i => i.PlannerId == plannerId)
                .ToList();
        }

        public PlannerItem GetPlannerItemById(int id)
        {
            return _context.PlannerItems
                .SingleOrDefault(i => i.Id == id);
        }
        public IEnumerable<int> GetUserIdsInPlanner(int plannerId)
        {
            var userIds = _context.PlannerUsers
                .Where(pu => pu.PlannerId == plannerId)
                .Select(i => i.UserId)
                .ToList();


            return userIds;
        }
    }
}
