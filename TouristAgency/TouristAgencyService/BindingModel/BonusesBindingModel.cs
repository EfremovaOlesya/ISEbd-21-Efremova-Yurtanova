using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.BindingModel
{    
    public class BonusesBindingModel
    {
        public int ClientId { get; set; }

        public decimal Bonus { get; set; }

        public decimal Shtraf { get; set; }

        public string Block { get; set; }
    }
}
