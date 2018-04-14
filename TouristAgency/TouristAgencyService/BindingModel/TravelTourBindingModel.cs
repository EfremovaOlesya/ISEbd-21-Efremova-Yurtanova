using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.BindingModel
{
    public class TravelTourBindingModel
    {
        public int Id { get; set; }

        public int TravelId { get; set; }

        public int TourId { get; set; }

        public int Count { get; set; }
    }
}
