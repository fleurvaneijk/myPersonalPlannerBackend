using System.Collections.Generic;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Repository.IRepository
{
    public interface IPlannerRepository
    {
        IEnumerable<int> GetPlannerIds(int userId);
        Planner GetPlanner(int plannerId);
        IEnumerable<PlannerItem> GetPlannerItems(int plannerId);
        
    }
}
