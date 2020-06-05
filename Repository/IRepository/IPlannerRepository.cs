using System.Collections.Generic;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Repository.IRepository
{
    public interface IPlannerRepository
    {
        IEnumerable<int> GetPlannerIds(int userId);
        Planner GetPlanner(int plannerId);
        IEnumerable<PlannerItem> GetPlannerItems(int plannerId);
        IEnumerable<int> GetUserIdsInPlanner(int plannerId);
        void AddPlanner(Planner planner);
        void AddUserToPlanner(int plannerId, int userId);
        void AddPlannerItem(PlannerItem plannerItem);
    }
}
