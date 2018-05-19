using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IvanAgencyService.BindingModel;
using IvanAgencyService.ViewModel;

namespace IvanAgencyService.Interfaces
{
    public interface ITravel
    {
        List<TravelViewModel> GetList();

        TravelViewModel GetElement(int id);

        void AddElement(TravelBindingModel model);

        void UpdElement(TravelBindingModel model);

        void DelElement(int id);
    }
}
