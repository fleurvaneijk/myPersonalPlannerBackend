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
        void RemoveUserFromPlanner(int plannerId, int userId);
        PlannerItem GetPlannerItem(int id);

        void RemovePlannerItem(PlannerItem plannerItem);
        void RemovePlanner(Planner planner);
        void UpdatePlannerItemIsDone(int itemId, bool isDone);
    }
}
