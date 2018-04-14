using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyService.BindingModel;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.Interfaces
{
   public interface ITravelService
    {
        List<TravelViewModel> GetList();

        TravelViewModel GetElement(int id);

        void AddElement(TravelBindingModel model);

        void UpdElement(TravelBindingModel model);

        void DelElement(int id);
    }
}
