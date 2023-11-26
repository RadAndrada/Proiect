using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [Display(Name = "Email")]        
        public string ContactEmail{ get; set; }
        public ICollection<Event>? Event { get; set; }
    }
}
