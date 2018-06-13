
using System.Collections.Generic;
namespace IvanAgencyService.ViewModel
{
    public class TravelViewModel
    {
        public int Id { get; set; }

        public string TravelName { get; set; }

        public decimal Price { get; set; }

        public List<TravelTourViewModel> TravelTours { get; set; }
    }
}
