using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Participant
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public ICollection<Payment>? Payments { get; set; }
    }
}
