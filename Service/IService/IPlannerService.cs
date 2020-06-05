#nullable enable
using System.Collections.Generic;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Service.IService
{
    public interface IPlannerService
    {
        IEnumerable<PlannerView> GetPlanners(int userId);
    }
}
