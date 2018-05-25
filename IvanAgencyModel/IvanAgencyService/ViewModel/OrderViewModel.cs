using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyService.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientFIO { get; set; }

        public int TravelId { get; set; }

        public string TravelName { get; set; }

        public int? AdminId { get; set; }

        public string AdminName { get; set; }

        public int Day { get; set; }

        public decimal Summa { get; set; }

        public string Status { get; set; }

        public string DateOfCreate { get; set; }

        public string DateOfImplement { get; set; }

        public int Bonuses { get; set; }

        public int Punishment { get; set; }
    }
}
