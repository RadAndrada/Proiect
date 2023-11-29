namespace Proiect.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryID { get; set; }
        public ICollection<EventCategory>? EventCategories { get; set; }
    }
}
