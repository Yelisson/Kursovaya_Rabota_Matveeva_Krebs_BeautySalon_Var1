using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ImplementationsBD
{
   public class ClientServiceBD:IClientService
    {
        private AbstractDataBaseContext context;

        public ClientServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public List<ClientViewModel> GetList()
        {
            List<ClientViewModel> result = context.Clients
                .Select(rec => new ClientViewModel
                {
                    id = rec.id,
                    clientFirstName = rec.clientFirstName,
                    clientSecondName = rec.clientSecondName,
                    password = rec.password,
                    mail = rec.mail,
                    number = rec.number
                })
                .ToList();
            return result;
        }

        public ClientViewModel GetElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new ClientViewModel
                {
                    id = element.id,
                    clientFirstName = element.clientFirstName,
                    clientSecondName = element.clientSecondName,
                     password = element.password,
                    mail = element.mail,
                    number = element.number
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.clientFirstName == model.clientFirstName && rec.clientSecondName==model.clientSecondName);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Clients.Add(new Client
            {
                clientFirstName=model.clientFirstName,
                clientSecondName=model.clientSecondName,
                password = model.password,
                mail = model.mail,
                number = model.number

            });
            context.SaveChanges();
        }

        public void UpdElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec =>
                                    rec.clientFirstName == model.clientFirstName && rec.clientSecondName == model.clientSecondName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Clients.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.clientFirstName = model.clientFirstName;
            element.clientSecondName = model.clientSecondName;
            element.password = model.password;
            element.mail = model.mail;
            element.number = model.number; 
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.id == id);
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
    }
}
