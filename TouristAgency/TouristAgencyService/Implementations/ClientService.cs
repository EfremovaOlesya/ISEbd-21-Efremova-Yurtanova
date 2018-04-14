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
   public class ClientService:IClientService
    {
        private TouristDbContext context;

        public ClientService(TouristDbContext context)
        {
            this.context = context;
        }

        public void AddElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.ClientFIO == model.ClientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким именем");
            }
            context.Clients.Add(new Client
            {
                Id = model.Id,
                ClientFIO = model.ClientFIO,            
                Bonus = 0,
                ClientLogin = model.ClientLogin,
                ClientPassword = model.ClientPassword,
                Orders = null,
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ClientViewModel GetElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ClientViewModel
                {
                    Id = element.Id,
                    ClientLogin = element.ClientLogin,
                    ClientFIO = element.ClientFIO,                  
                    Bonus = element.Bonus
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<ClientViewModel> GetList()
        {
            List<ClientViewModel> result = context.Clients.Select(rec => new ClientViewModel
            {
                Id = rec.Id,
                ClientLogin = rec.ClientLogin,
                ClientFIO = rec.ClientFIO,                
                Bonus = rec.Bonus
            })
                .ToList();
            return result;
        }

        public void UpdElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec =>
                                    rec.ClientFIO == model.ClientFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ClientFIO = model.ClientFIO;
            element.Id = model.Id;
            context.SaveChanges();
        }

        public string GenerateLogin(string fio)
        {
            char split = ' ';
            string firstName = fio.Substring(0, fio.IndexOf(split));

            fio = fio.Substring(fio.IndexOf(split) + 1);

            string name = fio.Substring(0, fio.IndexOf(split));

            string namePath = string.Empty;

            int position = 1;

            while (true)
            {
                if (name.Length > 0)
                {
                    namePath += name.First();
                    name = name.Substring(1);
                }
                else
                {
                    position++;
                }
                string login = firstName + "." + namePath + ((position > 1) ? position + "" : "");

                Client client = context.Clients.FirstOrDefault(rec => rec.ClientLogin.Equals(login));

                Worker worker = context.Workers.FirstOrDefault(rec => rec.WorkerLogin.Equals(login));
                if (client == null && worker == null)
                {
                    return login;
                }
            }
        }

        public void CreatePremium(BonusesBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.ClientId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Bonus += model.Bonus;
            context.SaveChanges();
        }

        public void DecreatePremium(BonusesBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.ClientId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Bonus = (element.Bonus - model.Shtraf > 0) ? element.Bonus - model.Shtraf : 0;
            context.SaveChanges();
        }
    }
}