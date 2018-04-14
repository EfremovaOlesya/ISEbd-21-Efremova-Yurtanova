using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyService.BindingModel;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.Interfaces
{
    public interface IClientService
    {
        List<ClientViewModel> GetList();

        ClientViewModel GetElement(int id);

        void AddElement(ClientBindingModel model);

        void UpdElement(ClientBindingModel model);

        void DelElement(int id);
    }
}
