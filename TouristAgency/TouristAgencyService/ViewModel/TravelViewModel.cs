using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.ViewModel
{
    public class TravelViewModel
    {
        public int Id { get; set; }

        public string TravelName { get; set; }

        public decimal PriceTravel { get; set; }

        public List<TravelTourViewModel> TravelTours { get; set; }
    }
}
