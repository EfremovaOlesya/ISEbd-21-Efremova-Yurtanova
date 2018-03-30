using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyModel
{
   public class Travel
    {
        public int Id { get; set; }

        [Required]
        public string TravelName { get; set; }

        public int DayCount { get; set; }

        public int AdultsCount { get; set; }

        public int ChildrenCount { get; set; }

        [Required]
        public decimal PriceTravel { get; set; }

        [ForeignKey("TravelId")]
        public virtual List<Order> Orders { get; set; }

        [ForeignKey("TravelId")]
        public virtual List<TravelTour> TravelTours { get; set; }
    }
}
