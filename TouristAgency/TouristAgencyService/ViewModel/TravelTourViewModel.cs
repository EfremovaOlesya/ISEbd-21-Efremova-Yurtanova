using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyService.ViewModel
{
    public class TravelTourViewModel
    {
        public int Id { get; set; }

        public int TravelId { get; set; }

        public int TourId { get; set; }

        public string TourName { get; set; }

        public decimal TourPrice { get; set; }
    }
}
