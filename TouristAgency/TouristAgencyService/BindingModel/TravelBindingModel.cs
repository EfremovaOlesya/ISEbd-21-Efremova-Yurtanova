using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyModel;

namespace TouristAgencyService.BindingModel
{
   public class TravelBindingModel
    {
        public int Id { get; set; }

        public string TravelName { get; set; }
      
        public decimal PriceTravel { get; set; }

        public List<TravelTourBindingModel> TravelTours { get; set; }
    }
}
