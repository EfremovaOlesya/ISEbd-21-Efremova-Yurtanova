
using System.Collections.Generic;
using IvanAgencyService.BindingModel;
using IvanAgencyService.ViewModel;
namespace IvanAgencyService.Interfaces
{
    public interface ITour
    {
        List<TourViewModel> GetList();

        TourViewModel GetElement(int id);

        void AddElement(TourBindingModel model);

        void UpdElement(TourBindingModel model);

        void DelElement(int id);
    }
}

