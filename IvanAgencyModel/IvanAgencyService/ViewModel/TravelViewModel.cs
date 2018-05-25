using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
