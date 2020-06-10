#nullable enable
namespace MyPersonalPlannerBackend.Model
{
    public class PlannerItem
    {
        public int Id { get; set; }
        public int PlannerId { get; set; }
        public Planner? Planner { get; set; }
        public int User { get; set; }
        public int Day { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsDone { get; set; }

        public PlannerItem()
        {

        }
    }
}
