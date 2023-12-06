﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Proiect.Models
{
    public class Event
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
       
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Ticket Price")]
        [Column(TypeName ="decimal(5,2)")]
        public decimal Price  { get; set; }
        [Display(Name = "Email Contact")]
        public int? ContactEmail { get; set; }
        public Contact? Contact { get; set; }
        public int? PaymentID { get; set; }
        public Payment? Payment { get; set; }

        public ICollection<EventCategory>? EventCategories { get; set; }
        public List<Category> Categories => EventCategories?.Select(ec => ec.Category).ToList();

    }
}
