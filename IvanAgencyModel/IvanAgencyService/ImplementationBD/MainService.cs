using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IvanAgencyModel;
using IvanAgencyService.BindingModel;
using IvanAgencyService.Interfaces;
using IvanAgencyService.ViewModel;

namespace IvanAgencyService.ImplementationBD
{
    public class MainService : IMain
    {
        private IvanSuDbContext context;

        public MainService(IvanSuDbContext context)
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
                    AdminId = rec.AdminId,
                    DateOfCreate = SqlFunctions.DateName("dd", rec.DateOfCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateOfCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateOfCreate),
                    DateOfImplement = rec.DateOfImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateOfImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateOfImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateOfImplement.Value),
                    Status = rec.Status.ToString(),
                    Day = rec.Day,
                    Summa = rec.Summa,
                    ClientFIO = rec.Client.ClientFIO,
                    TravelName = rec.Travel.TravelName,
                    AdminName = rec.Admin.AdminFIO,
                    Bonuses = rec.Client.Bonuses
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
                DateOfCreate = DateTime.Now,
                Day = model.Day,
                Summa = model.Summa,
                Status = StatusOfOrder.Не_оплачен
            });
            context.SaveChanges();
        }



        public void FinishOrder(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = StatusOfOrder.Оплачен;
            element.DateOfImplement = DateTime.Now;
            context.SaveChanges();
        }

        public void PayOrder(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = StatusOfOrder.Оплачен_частично;
            context.SaveChanges();
        }
        public void AddBonuses(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.Bonuses = model.Bonuses;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void AddPunishment(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.Punishment = model.Punishment;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
