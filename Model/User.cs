using System.Collections.Generic;

namespace MyPersonalPlannerBackend.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string AgendaLink { get; set; }
        public IList<PlannerUser> PlannerUsers { get; set; }

        public User()
        {

        }
    }
}
