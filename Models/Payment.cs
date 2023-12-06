using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public int? ParticipantID { get; set; }
        public Participant? Participant { get; set; }
       
        [ForeignKey("Event")]
        public int? EventID { get; set; }
        public Event? Event { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
    }
}
