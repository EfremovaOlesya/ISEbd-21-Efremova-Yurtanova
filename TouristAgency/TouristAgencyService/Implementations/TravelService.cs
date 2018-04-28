using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyModel;
using TouristAgencyService.BindingModel;
using TouristAgencyService.Interfaces;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.ImplementationsList
{
    public class TravelService : ITravelService
    {
        private TouristDbContext context;

        public TravelService(TouristDbContext context)
        {
            this.context = context;
        }

        public void AddElement(TravelBindingModel model)
        {
            Travel element = context.Travels.FirstOrDefault(rec => rec.TravelName == model.TravelName);
            if (element != null)
            {
                throw new Exception("Уже есть путешествие с таким названием");
            }
            context.Travels.Add(new Travel
            {
                TravelName = model.TravelName,
                PriceTravel = model.PriceTravel
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Travel element = context.Travels.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Travels.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
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
                    PriceTravel = element.PriceTravel
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<TravelViewModel> GetList()
        {
            List<TravelViewModel> result = context.Travels.Select(rec => new TravelViewModel
            {
                Id = rec.Id,
                TravelName = rec.TravelName,
                PriceTravel = rec.PriceTravel
            })
            .ToList();
            return result;
        }

        public void UpdElement(TravelBindingModel model)
        {
            Travel element = context.Travels.FirstOrDefault(rec =>
            rec.TravelName == model.TravelName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть путешествие с таким названием");
            }
            element = context.Travels.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.TravelName = model.TravelName;
            element.Id = model.Id;
            context.SaveChanges();
        }
    }
}