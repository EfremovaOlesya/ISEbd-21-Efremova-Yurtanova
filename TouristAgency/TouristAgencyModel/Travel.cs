using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyModel
{
    public class Travel
    {
        public int Id { get; set; }

        [Required]
        public string TravelName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("TravelId")]
        public virtual List<Order> Orders { get; set; }

        [ForeignKey("TravelId")]
        public virtual List<TravelTour> TravelTours { get; set; }
    }
}
