using IvanAgencyService.BindingModel;
using IvanAgencyService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanAgencyService.Interfaces
{
   public interface IReport
    {
        void SaveTourPriceW(ReportBindingModel model);

        void SaveTourPriceE(ReportBindingModel model);

        List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(ReportBindingModel model);

        void SaveTravelPriceW(ReportBindingModel model);

        void SaveTravelPriceE(ReportBindingModel model);
    }
}
