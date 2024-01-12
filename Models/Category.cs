namespace Proiect.Models
{
    public class Category
    {
        public int ID { get; set; }
        public int CategoryID { get; set; } 

        public string CategoryName { get; set; }
        public ICollection<EventCategory>? EventCategories { get; set; }
        public List<Event> Events => EventCategories?.Select(ec => ec.Event).ToList();
    }
}
