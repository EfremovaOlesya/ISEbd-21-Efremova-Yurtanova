using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.ViewModel
{
   public class ClientViewModel
    {
        public int Id { get; set; }

        public string ClientFIO { get; set; }

        public string ClientLogin { get; set; }

        public decimal Bonus { get; set; }

        public decimal Shtraf { get; set; }

        public string Block { get; set; }
    }
}
