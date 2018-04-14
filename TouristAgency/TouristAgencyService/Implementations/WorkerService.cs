using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgencyModel;
using TouristAgencyService.BindingModel;
using TouristAgencyService.Interfaces;
using TouristAgencyService.ViewModel;

namespace TouristAgencyService.ImplementationsList
{
   public class WorkerService: IWorkerService
    {
        private TouristDbContext context;

        public WorkerService(TouristDbContext context)
        {
            this.context = context;
        }

        public void AddElement(WorkerBindingModel model)
        {
            Worker element = context.Workers.FirstOrDefault(rec => rec.WorkerFIO == model.WorkerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть админ с таким ФИО");
            }
            context.Workers.Add(new Worker
            {
                WorkerFIO = model.WorkerFIO,
                WorkerLogin = model.WorkerLogin,
                WorkerPassword = model.WorkerPassword
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Worker element = context.Workers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Workers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WorkerViewModel GetElement(int id)
        {
            Worker element = context.Workers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new WorkerViewModel
                {
                    Id = element.Id,
                    WorkerFIO = element.WorkerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<WorkerViewModel> GetList()
        {
            List<WorkerViewModel> result = context.Workers.Select(rec => new WorkerViewModel
            {
                Id = rec.Id,
                WorkerFIO = rec.WorkerFIO,
                WorkerLogin = rec.WorkerLogin
            })
                .ToList();
            return result;
        }

        public void UpdElement(WorkerBindingModel model)
        {
            Worker element = context.Workers.FirstOrDefault(rec =>
                                    rec.WorkerFIO == model.WorkerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть админ с таким ФИО");
            }
            element = context.Workers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.WorkerFIO = model.WorkerFIO;
            context.SaveChanges();
        }
    }
}