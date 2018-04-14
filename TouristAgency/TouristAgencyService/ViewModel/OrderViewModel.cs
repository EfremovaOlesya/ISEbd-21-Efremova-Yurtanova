using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.ViewModel
{
   public class OrderViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientFIO { get; set; }

        public int TravelId { get; set; }

        public string TravelName { get; set; }

        public int? WorkerId { get; set; }

        public string WorkerFIO { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public int DayCount { get; set; }

        public int AdultsCount { get; set; }

        public int ChildrenCount { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateImplement { get; set; }
    }
}
