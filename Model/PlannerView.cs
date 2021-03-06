using System.Collections.Generic;

namespace MyPersonalPlannerBackend.Model
{
    public class PlannerView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Owner { get; set; }

        public IList<PlannerItem> PlannerItems { get; set; }
        public IList<User> Users { get; set; }

        public PlannerView()
        {

        }
    }
}
