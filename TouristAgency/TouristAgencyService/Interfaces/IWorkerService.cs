using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyService.BindingModel;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.Interfaces
{
   public interface IWorkerService
    {
        List<WorkerViewModel> GetList();

        WorkerViewModel GetElement(int id);

        void AddElement(WorkerBindingModel model);

        void UpdElement(WorkerBindingModel model);

        void DelElement(int id);
    }
}
