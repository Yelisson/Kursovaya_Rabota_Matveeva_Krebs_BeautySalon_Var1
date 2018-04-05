using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ImplementationsList
{
   public class ClientServiceList:IClientService
    {
        private DataListSingleton source;

        public ClientServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ClientViewModel> GetList()
        {
            List<ClientViewModel> result = source.Clients
                .Select(rec => new ClientViewModel
                {
                    id = rec.id,
                    clientFirstName = rec.clientFirstName,
                    clientSecondName=rec.clientSecondName
                })
                .ToList();
            return result;
        }

        public ClientViewModel GetElement(int id)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new ClientViewModel
                {
                    id = element.id,
                    clientFirstName = element.clientFirstName,
                    clientSecondName=element.clientSecondName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ClientBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.clientSecondName == model.clientSecondName && rec.clientFirstName==model.clientFirstName);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Clients.Count > 0 ? source.Clients.Max(rec => rec.id) : 0;
            source.Clients.Add(new Client
            {
                id = maxId + 1,
                clientFirstName = model.clientFirstName,
                clientSecondName=model.clientSecondName
            });
        }

        public void UpdElement(ClientBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec =>
                                    rec.clientSecondName == model.clientSecondName && rec.clientFirstName == model.clientFirstName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Clients.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.clientFirstName = model.clientFirstName;
            element.clientSecondName = model.clientSecondName;
        }

        public void DelElement(int id)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.Clients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
