using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.BindingModel
{
    public class OrderBindingModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int TravelId { get; set; }

        public int? WorkerId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public int DayCount { get; set; }

        public int AdultsCount { get; set; }

        public int ChildrenCount { get; set; }
    }
}
