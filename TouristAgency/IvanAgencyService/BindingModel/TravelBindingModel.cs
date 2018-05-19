using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
