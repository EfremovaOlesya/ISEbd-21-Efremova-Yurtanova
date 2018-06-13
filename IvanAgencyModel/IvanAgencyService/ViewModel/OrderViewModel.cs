
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

        public decimal SummaOplaty { get; set; }

        public int Bonus { get; set; }

        public string Status { get; set; }
      
        public string DateOfCreate { get; set; }

        public string DateOfImplement { get; set; }
    }
}
