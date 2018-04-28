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
    public class TourService : ITourService
    {
        private TouristDbContext context;

        public TourService(TouristDbContext context)
        {
            this.context = context;
        }

        public List<TourViewModel> GetList()
        {
            List<TourViewModel> result = context.Tours
            .Select(rec => new TourViewModel
            {
                Id = rec.Id,
                TourName = rec.TourName
            })
            .ToList();
            return result;
        }

        public TourViewModel GetElement(int id)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TourViewModel
                {
                    Id = element.Id,
                    TourName = element.TourName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(TourBindingModel model)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.TourName == model.TourName);
            if (element != null)
            {
                throw new Exception("Уже есть тур с таким названием");
            }
            context.Tours.Add(new Tour
            {
                TourName = model.TourName
            });
            context.SaveChanges();
        }

        public void UpdElement(TourBindingModel model)
        {
            Tour element = context.Tours.FirstOrDefault(rec =>
            rec.TourName == model.TourName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть тур с таким названием");
            }
            element = context.Tours.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.TourName = model.TourName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Tours.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}