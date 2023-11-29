
namespace Proiect.Models
{
    public class EventData
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<EventCategory> EventCategories { get; set; }

    }
}
