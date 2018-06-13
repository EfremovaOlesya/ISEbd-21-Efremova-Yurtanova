
using System.Collections.Generic;
using IvanAgencyService.BindingModel;
using IvanAgencyService.ViewModel;
namespace IvanAgencyService.Interfaces
{
    public interface IAdmin
    {
        List<AdminViewModel> GetList();

        AdminViewModel GetElement(int id);

        void AddElement(AdminBindingModel model);

        void UpdElement(AdminBindingModel model);

        void DelElement(int id);
    }
}