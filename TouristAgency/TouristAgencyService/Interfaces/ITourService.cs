using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyService.BindingModel;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.Interfaces
{
   public interface ITourService
    {
        List<TourViewModel> GetList();

        TourViewModel GetElement(int id);

        void AddElement(TourBindingModel model);

        void UpdElement(TourBindingModel model);

        void DelElement(int id);
    }
}
