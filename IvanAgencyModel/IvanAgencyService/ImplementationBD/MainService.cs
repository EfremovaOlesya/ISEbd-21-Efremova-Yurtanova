using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
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

        public OrderViewModel GetElement(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new OrderViewModel
                {
                    Id = element.Id,
                    ClientId = element.ClientId,                                
                    Status = element.Status,                 
                    Summa = element.Summa,
                    SummaOplaty = element.SummaOplaty,
                    Bonus = element.Bonus,

                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<OrderViewModel> GetList(int id)
        {
            List<OrderViewModel> result = context.Orders
                .Where(rec => rec.Client.Id == id)
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
                    Status = rec.Status,
                    Day = rec.Day,
                    Summa = rec.Summa,
                    SummaOplaty = rec.SummaOplaty,
                    Bonus = rec.Bonus,
                    ClientFIO = rec.Client.ClientFIO,
                    TravelName = rec.Travel.TravelName,
                    AdminName = rec.Admin.AdminFIO
                })
                .ToList();
            return result;
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
                    Status = rec.Status,
                    Day = rec.Day,
                    Summa = rec.Summa,
                    SummaOplaty = rec.SummaOplaty,
                    Bonus = rec.Bonus,
                    ClientFIO = rec.Client.ClientFIO,
                    TravelName = rec.Travel.TravelName,
                    AdminName = rec.Admin.AdminFIO
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
                Status = model.Status
            });
            context.SaveChanges();
        }
    
        public void PayOrder(OrderBindingModel model)
        {           
                try
                {
                    Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.SummaOplaty = model.SummaOplaty;
                    element.Status = model.Status;
                    element.DateOfImplement = DateTime.Now;
                    context.SaveChanges();
            }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        public void BonusOrder(OrderBindingModel model)
        {
            try
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.Bonus = model.Bonus;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateOrder(OrderBindingModel model)
        {
            try
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.Summa = model.Summa;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    }

