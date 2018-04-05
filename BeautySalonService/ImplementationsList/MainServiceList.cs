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

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = source.Orders
                .Select(rec => new OrderViewModel
                {
                    id = rec.id,
                    clientId = rec.clientId,
                    serviceId=rec.serviceId,
                    adminId = rec.adminId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    status = rec.status.ToString(),
                    number=rec.number,
                    clientName = source.Clients
                                    .FirstOrDefault(recC => recC.id == rec.clientId)?.clientFirstName,
                    serviceName = source.Services
                                    .FirstOrDefault(recP => recP.id == rec.serviceId)?.serviceName
                })
                .ToList();
            return result;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.id) : 0;
            source.Orders.Add(new Order
            {
                id = maxId + 1,
                clientId = model.clientId,
                serviceId = model.serviceId,
                clientName=model.clientName,
                serviceName=model.serviceName,
                DateCreate = DateTime.Now,
                status = OrderStatus.Принят
            });
        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            var productComponents = source.ServiceResources.Where(rec => rec.serviceId == element.serviceId);
            foreach (var productComponent in productComponents)
            {
                int countOnDeliverys = source.DeliveryResources
                                            .Where(rec => rec.resourceId == productComponent.resourceId)
                                            .Sum(rec => rec.count);
                if (countOnDeliverys < productComponent.count)
                {
                    var resourceName = source.Resources
                                    .FirstOrDefault(rec => rec.id == productComponent.resourceId);
                    throw new Exception("Не достаточно компонента " + resourceName?.resourceName +
                        " требуется " + productComponent.count + ", в наличии " + countOnDeliverys);
                }
            }
            foreach (var productComponent in productComponents)
            {
                int countOnDeliverys = productComponent.count;
                var deliveryComponents = source.DeliveryResources
                                            .Where(rec => rec.resourceId == productComponent.resourceId);
                foreach (var stockComponent in deliveryComponents)
                {

                    if (stockComponent.count >= countOnDeliverys)
                    {
                        stockComponent.count -= countOnDeliverys;
                        break;
                    }
                    else
                    {
                        countOnDeliverys -= stockComponent.count;
                        stockComponent.count = 0;
                    }
                }
            }
            element.adminId = model.adminId;
            element.status = OrderStatus.Выполняетcя;
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
        }
    }
}
