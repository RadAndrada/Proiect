namespace Proiect.Models
{
    public class EventCategory
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public Event Event{ get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
