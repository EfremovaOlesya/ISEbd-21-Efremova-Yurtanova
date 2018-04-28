using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyService.BindingModel;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.Interfaces
{
    public interface IMainService
    {
        List<OrderViewModel> GetList();

        void CreateOrder(OrderBindingModel model);       

        void PayOrder(OrderBindingModel model);
    }
}
