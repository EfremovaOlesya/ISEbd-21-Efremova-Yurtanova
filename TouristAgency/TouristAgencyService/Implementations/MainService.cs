using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyModel;
using TouristAgencyService.BindingModel;
using TouristAgencyService.Interfaces;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.ImplementationsList
{   
       public class MainService : IMainService
        {
        private TouristDbContext context;

        public MainService(TouristDbContext context)
        {
            this.context = context;
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    TravelId = rec.TravelId,
                    WorkerId = rec.WorkerId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateImplement = rec.DateImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Summ = rec.Summ,
                    ClientFIO = rec.Client.ClientFIO,
                   TravelName = rec.Travel.TravelName,
                    WorkerFIO = rec.Worker.WorkerFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            context.Orders.Add(new Order
            {
                ClientId = model.ClientId,
                TravelId = model.TravelId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Summ = model.Summ,
                Status = PaymentState.Не_оплачено
            });
            context.SaveChanges();
        }             

        public void PayOrder(OrderBindingModel model)
        {
            context.Orders.Add(new Order
            {              
                Summ = model.Summ,
                Status = PaymentState.Принят
            });                       
            context.SaveChanges();
        }

       
    }
}