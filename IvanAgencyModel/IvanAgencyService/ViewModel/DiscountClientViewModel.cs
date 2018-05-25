using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyService.ViewModel
{
    public class DiscountClientViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientFIO { get; set; }

        public int DiscountId { get; set; }

        public decimal Bonuses { get; set; }

        public decimal Punishment { get; set; }
    }
}
