using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyModel
{
   public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int TravelId { get; set; }

        public int? WorkerId { get; set; }

        public int Count { get; set; }

        public decimal Summ { get; set; }

        public int DayCount { get; set; }

        public int AdultsCount { get; set; }

        public int ChildrenCount { get; set; }

        public PaymentState Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Client Client { get; set; }   

        public virtual Travel Travel { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
