using IvanAgencyService.BindingModel;
using IvanAgencyService.ViewModel;
using System.Collections.Generic;
namespace IvanAgencyService.Interfaces
{
   public interface IReport
    {
        void SaveTourPriceW(ReportBindingModel model);

        void SaveTourPriceE(ReportBindingModel model);

        List<ClientOrdersViewModel> GetClientOrders(int id, ReportBindingModel model);

        void SaveClientOrders(int id, ReportBindingModel model);

        void SaveTravelPriceW(ReportBindingModel model);

        void SaveTravelPriceE(ReportBindingModel model);

        List<ClientOrdersViewModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(ReportBindingModel model);


    }
}
