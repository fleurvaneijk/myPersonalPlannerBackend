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

        public void AddPlanner(Planner planner)
        {
            _context.Planners.Add(planner);
            _context.SaveChanges();
            AddUserToPlanner(planner.Id, planner.Owner);
        }

        public void AddUserToPlanner(int plannerId, int userId)
        {
            _context.PlannerUsers.Add(new PlannerUser()
            {
                PlannerId = plannerId,
                UserId = userId
            });
            _context.SaveChanges();
        }

        public void AddPlannerItem(PlannerItem plannerItem)
        {
            _context.PlannerItems.Add(plannerItem);
            _context.SaveChanges();
        }

        public void RemoveUserFromPlanner(int plannerId, int userId)
        {
            _context.PlannerUsers.Remove(new PlannerUser()
            {
                PlannerId = plannerId,
                UserId = userId
            });
            _context.SaveChanges();
        }

        public PlannerItem GetPlannerItem(int id)
        {
            return _context.PlannerItems.Find(id);
        }

        public void RemovePlannerItem(PlannerItem plannerItem)
        {
            _context.PlannerItems.Remove(plannerItem);
            _context.SaveChanges();
        }

        public void RemovePlanner(Planner planner)
        {
            _context.Planners.Remove(planner);
            _context.SaveChanges();
        }

        public void UpdatePlannerItemIsDone(int itemId, bool isDone)
        {
            var item = _context.PlannerItems.Find(itemId);
            item.IsDone = isDone;
            _context.SaveChanges();
        }
    }
}
