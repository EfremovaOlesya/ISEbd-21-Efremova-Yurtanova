
using System.Collections.Generic;
namespace IvanAgencyService.BindingModel
{
    public class TravelBindingModel
    {
        public int Id { get; set; }

        public string TravelName { get; set; }

        public decimal Price { get; set; }

        public List<TravelTourBindingModel> TravelTours { get; set; }
    }
}
