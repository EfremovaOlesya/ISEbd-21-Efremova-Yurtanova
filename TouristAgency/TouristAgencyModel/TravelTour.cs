using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyModel
{
    public class TravelTour
    {
        public int Id { get; set; }

        public int TravelId { get; set; }

        public int TourId { get; set; }

        public decimal TourPrice { get; set; }

        public virtual Travel Travel { get; set; }

        public virtual Tour Tour { get; set; }
    }
}