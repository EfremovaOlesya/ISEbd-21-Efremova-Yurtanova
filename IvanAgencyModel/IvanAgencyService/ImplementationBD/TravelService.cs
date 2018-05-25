using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IvanAgencyModel;
using IvanAgencyService.BindingModel;
using IvanAgencyService.Interfaces;
using IvanAgencyService.ViewModel;

namespace IvanAgencyService.ImplementationBD
{
    public class TravelService : ITravel
    {
        private IvanSuDbContext context;

        public TravelService(IvanSuDbContext context)
        {
            this.context = context;
        }

        public List<TravelViewModel> GetList()
        {
            List<TravelViewModel> result = context.Travels
                .Select(rec => new TravelViewModel
                {
                    Id = rec.Id,
                    TravelName = rec.TravelName,
                    Price = rec.Price,
                    TravelTours = context.TravelTours
                            .Where(recPC => recPC.TravelId == rec.Id)
                            .Select(recPC => new TravelTourViewModel
                            {
                                Id = recPC.Id,
                                TravelId = recPC.TravelId,
                                TourId = recPC.TourId,
                                TourName = recPC.Tour.TourName,
                                TourPrice = recPC.TourPrice
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public TravelViewModel GetElement(int id)
        {
            Travel element = context.Travels.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TravelViewModel
                {
                    Id = element.Id,
                    TravelName = element.TravelName,
                    Price = element.Price,
                    TravelTours = context.TravelTours
                            .Where(recPC => recPC.TravelId == element.Id)
                            .Select(recPC => new TravelTourViewModel
                            {
                                Id = recPC.Id,
                                TravelId = recPC.TravelId,
                                TourId = recPC.TourId,
                                TourName = recPC.Tour.TourName,
                                TourPrice = recPC.TourPrice
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(TravelBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec => rec.TravelName == model.TravelName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Travel
                    {
                        TravelName = model.TravelName,
                        Price = model.Price
                    };
                    context.Travels.Add(element);
                    context.SaveChanges();
                    var groupComponents = model.TravelTours
                                                .GroupBy(rec => rec.TourId)
                                                .Select(rec => new
                                                {
                                                    ComponentId = rec.Key,
                                                    Count = rec.Sum(r => r.TourPrice)
                                                });
                    foreach (var groupComponent in groupComponents)
                    {
                        context.TravelTours.Add(new TravelTour
                        {
                            TravelId = element.Id,
                            TourId = groupComponent.ComponentId,
                            TourPrice = groupComponent.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(TravelBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec =>
                                        rec.TravelName == model.TravelName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Travels.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.TravelName = model.TravelName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    var compIds = model.TravelTours.Select(rec => rec.TourId).Distinct();
                    var updateComponents = context.TravelTours
                                                    .Where(rec => rec.TravelId == model.Id &&
                                                        compIds.Contains(rec.TourId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.TourPrice = model.TravelTours
                                                        .FirstOrDefault(rec => rec.Id == updateComponent.Id).TourPrice;
                    }
                    context.SaveChanges();
                    context.TravelTours.RemoveRange(
                                        context.TravelTours.Where(rec => rec.TravelId == model.Id &&
                                                                            !compIds.Contains(rec.TourId)));
                    context.SaveChanges();
                    var groupComponents = model.TravelTours
                                                .Where(rec => rec.Id == 0)
                                                .GroupBy(rec => rec.TourId)
                                                .Select(rec => new
                                                {
                                                    ComponentId = rec.Key,
                                                    Count = rec.Sum(r => r.TourPrice)
                                                });
                    foreach (var groupComponent in groupComponents)
                    {
                        TravelTour elementPC = context.TravelTours
                                                .FirstOrDefault(rec => rec.TravelId == model.Id &&
                                                                rec.TourId == groupComponent.ComponentId);
                        if (elementPC != null)
                        {
                            elementPC.TourPrice += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.TravelTours.Add(new TravelTour
                            {
                                TravelId = model.Id,
                                TourId = groupComponent.ComponentId,
                                TourPrice = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        context.TravelTours.RemoveRange(
                                            context.TravelTours.Where(rec => rec.TravelId == id));
                        context.Travels.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
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
