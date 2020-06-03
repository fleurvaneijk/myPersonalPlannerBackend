namespace MyPersonalPlannerBackend.Model
{
    public class PlannerUser
    {
        public int PlannerId { get; set; }
        public Planner Planner { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public PlannerUser()
        {

        }
    }
}
