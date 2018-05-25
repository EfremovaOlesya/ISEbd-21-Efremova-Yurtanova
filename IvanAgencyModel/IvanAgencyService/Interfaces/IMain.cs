using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IvanAgencyService.BindingModel;
using IvanAgencyService.ViewModel;

namespace IvanAgencyService.Interfaces
{
    public interface IMain
    {
        List<OrderViewModel> GetList();

        void CreateOrder(OrderBindingModel model);

        void FinishOrder(int id);

        void PayOrder(int id);

        void AddBonuses(OrderBindingModel model);

        void AddPunishment(OrderBindingModel model);
    }
}
