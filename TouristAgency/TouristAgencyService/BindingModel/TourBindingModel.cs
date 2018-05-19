using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyService.BindingModel
{
    public class TourBindingModel
    {
        public int Id { get; set; }

        public string TourName { get; set; }

        public decimal PriceTour { get; set; }
    }
}
