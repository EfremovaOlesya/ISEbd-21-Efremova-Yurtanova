using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyModel
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int TravelId { get; set; }

        public int? AdminId { get; set; }

        public int Day { get; set; }

        public decimal Summa { get; set; }

        public StatusOfOrder Status { get; set; }

        public DateTime DateOfCreate { get; set; }

        public DateTime? DateOfImplement { get; set; }

        public virtual Client Client { get; set; }

        public virtual Travel Travel { get; set; }

        public virtual Admin Admin { get; set; }

        public int Bonuses { get; set; }

        public int Punishment { get; set; }
    }
}