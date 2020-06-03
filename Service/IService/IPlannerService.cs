#nullable enable
using System.Collections.Generic;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Service.IService
{
    public interface IPlannerService
    {
        IEnumerable<int> GetPlannerIds(int userId);
        Planner GetPlanner(in int plannerId);
        IEnumerable<PlannerItem> GetPlannerItems(int plannerId);
        IEnumerable<User> GetUsersInPlanner(int plannerId);
    }
}
