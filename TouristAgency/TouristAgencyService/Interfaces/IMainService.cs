using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyService.BindingModel;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.Interfaces
{
    interface IMainService
    {
        List<OrderViewModel> GetList();

        void CreateOrder(OrderBindingModel model);

        void PartiallyPaid(int id);

        void PayOrder(int id);
    }
}
