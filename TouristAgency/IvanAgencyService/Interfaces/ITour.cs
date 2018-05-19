using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

