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
                    adminId = rec.adminId,
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
            /*
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
                        " требуется " + productComponent.count + ", в наличии " + countOnDeliverys + ". Оформите заявку на доставку ресурсов.");
                }
            }
            */
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
    }
}
