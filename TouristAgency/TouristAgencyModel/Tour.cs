using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyModel
{
    public class Tour
    {
        public int Id { get; set; }

        [Required]
        public string TourName { get; set; }

        [Required]
        public decimal PriceTour { get; set; }

        [ForeignKey("TourId")]
        public virtual List<TravelTour> TravelTours { get; set; }
    }
}

