using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Participant
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage ="Prenumele trebuie sa inceapa cu majuscula!!!")]

        [StringLength(30, MinimumLength = 3)]
        public string? FirstName { get; set; }
        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [StringLength(30, MinimumLength = 3)]

        public string? LastName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Adresa de email trebuie să fie de forma 'user@example.com'")]

        public string? Email { get; set; }
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
