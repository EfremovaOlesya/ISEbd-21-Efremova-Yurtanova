using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyService.ViewModel
{
   public class ClientOrdersViewModel
    {
        public string ClientName { get; set; }

        public string DateOfCreate { get; set; }

        public string TravelName { get; set; }

        public int Day { get; set; }

        public decimal Summa { get; set; }

        public string Status { get; set; }
    }
}
