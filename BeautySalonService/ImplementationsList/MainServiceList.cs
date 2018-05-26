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
   public class MainServiceList:IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public int GetIdClient(string fn, string sn, string p)
        {
            List<ClientViewModel> result = source.Clients
                .Where(rec => rec.clientFirstName == fn && rec.clientSecondName == sn && rec.password == p)
                .Select(rec => new ClientViewModel
                {
                    id = rec.id,
                })
                .ToList();
            int ind = -1;
            if (result.Count != 0)
            {
                ind = result.ElementAt(0).id;
            }
            return ind;
        }
        public List<OrderViewModel> GetListForClient(int id)
        {
            List<OrderViewModel> result = source.Orders
                .Where(rec => rec.clientId == id)
                .Select(rec => new OrderViewModel
                {
                    id = rec.id,
                    clientId = rec.clientId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    status = rec.status.ToString(),
                    number = rec.number,
                    clientName = source.Clients
                                    .FirstOrDefault(recC => recC.id == rec.clientId)?.clientFirstName
                })
                .ToList();
            return result;
        }
        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = source.Orders
                .Select(rec => new OrderViewModel
                {
                    id = rec.id,
                    clientId = rec.clientId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    status = rec.status.ToString(),
                    number=rec.number,
                    clientName = source.Clients
                                    .FirstOrDefault(recC => recC.id == rec.clientId)?.clientFirstName
                })
                .ToList();
            return result;
        }

        public void TakeOrderInWork(int id)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = OrderStatus.Ожидание;
        }

        public void FinishOrder(int id)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = OrderStatus.Готов;
        }

        public void PayOrder(int id)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = OrderStatus.Оплачен;
        }

        public void PutComponent(DeliveryResourceBindingModel model)
        {
            DeliveryResource element = source.DeliveryResources
                                                .FirstOrDefault(rec => rec.deliveryId == model.deliveryId &&
                                                                    rec.resourceId == model.resourceId);
            if (element != null)
            {
                element.count += model.count;
            }
            else
            {
                int maxId = source.DeliveryResources.Count > 0 ? source.DeliveryResources.Max(rec => rec.id) : 0;
                source.DeliveryResources.Add(new DeliveryResource
                {
                    id = ++maxId,
                    deliveryId = model.deliveryId,
                    resourceId = model.resourceId,
                    count = model.count
                });
            }
            source.Resources.FirstOrDefault(res => res.id == model.resourceId).sumCount += model.count;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].id > maxId)
                {
                    maxId = source.Clients[i].id;
                }
            }
            source.Orders.Add(new Order
            {
                id = maxId + 1,
                clientId = model.clientId,
                serviceId = model.serviceId,
                DateCreate = DateTime.Now,
                status = OrderStatus.Принят
            });
        }

    }
}
