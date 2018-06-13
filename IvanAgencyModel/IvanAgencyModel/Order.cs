using System;
using System.Runtime.Serialization;
namespace IvanAgencyModel
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int TravelId { get; set; }

        [DataMember]
        public int? AdminId { get; set; }

        [DataMember]
        public int Day { get; set; }

        [DataMember]
        public decimal Summa { get; set; }

        [DataMember]
        public decimal SummaOplaty { get; set; }

        [DataMember]
        public int Bonus { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public DateTime DateOfCreate { get; set; }

        [DataMember]
        public DateTime? DateOfImplement { get; set; }

        public virtual Client Client { get; set; }

        public virtual Travel Travel { get; set; }

        public virtual Admin Admin { get; set; }
    }
}