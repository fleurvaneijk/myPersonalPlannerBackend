#nullable enable
using System.Collections.Generic;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Service.IService
{
    public interface IPlannerService
    {
        IEnumerable<PlannerView> GetPlanners(int userId);
        void CreatePlanner(int userId, string title);
        void AddUserToPlanner(int loggedInUserId, UserPlanner model);
        void AddPlannerItem(int loggedInUserId, PlannerItem item);
        void RemoveUserFromPlanner(int loggedInUserId, UserPlanner model);
        void RemoveItemFromPlanner(User user, int itemId);
        void RemovePlanner(User user, int id);
        void SetDonePlannerItem(User loggedInUser, int itemId, bool isDone);
    }
}
