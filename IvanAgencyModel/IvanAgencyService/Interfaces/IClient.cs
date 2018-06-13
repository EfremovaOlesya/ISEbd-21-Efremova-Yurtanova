
using System.Collections.Generic;
using IvanAgencyService.BindingModel;
using IvanAgencyService.ViewModel;
namespace IvanAgencyService.Interfaces
{
    public interface IClient
    {
        List<ClientViewModel> GetList();

        ClientViewModel GetElement(int id);

        void AddElement(ClientBindingModel model);

        void UpdElement(ClientBindingModel model);

        void DelElement(int id);
    }
}
