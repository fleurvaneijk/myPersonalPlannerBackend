using System.ComponentModel.DataAnnotations;

namespace MyPersonalPlannerBackend.Model
{
    public class Agenda
    {
        [Required]
        public string AgendaLink { get; set; }
    }
}