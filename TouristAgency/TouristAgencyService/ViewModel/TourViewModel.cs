using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.ViewModel
{
   public class TourViewModel
    {
        public int Id { get; set; }

        public string TourName { get; set; }

        public int TravelId { get; set; }

        public decimal PriceTour { get; set; }
    }
}
