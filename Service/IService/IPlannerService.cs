#nullable enable
using System.Collections.Generic;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Service.IService
{
    public interface IPlannerService
    {
        IEnumerable<PlannerView> GetPlanners(int userId);
        void CreatePlanner(int userId, string title);
        void AddUserToPlanner(int loggedInUserId, AddUserToPlanner model);
        void AddPlannerItem(int loggedInUserId, PlannerItem item);
        void RemovePlannerFromUser(in int loggedInUserId, AddUserToPlanner model);
        void RemoveItemFromPlanner(User user, int itemId);
        void RemovePlanner(User user, int id);
        void MarkAllItemsAsNotDone();
    }
}
