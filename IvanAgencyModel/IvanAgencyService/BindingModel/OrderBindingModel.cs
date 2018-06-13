

namespace IvanAgencyService.BindingModel
{
    public class OrderBindingModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int TravelId { get; set; }

        public int? AdminId { get; set; }

        public int Day { get; set; }

        public decimal Summa { get; set; }

        public decimal SummaOplaty { get; set; }

        public int Bonus { get; set; }

        public string Status { get; set; }

    }
}
