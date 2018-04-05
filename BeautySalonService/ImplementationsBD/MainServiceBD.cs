using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ImplementationsBD
{
    public class MainServiceBD:IMainService
    {
        private AbstractDataBaseContext context;

        public MainServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders
                .Select(rec => new OrderViewModel
                {
                    id = rec.id,
                    clientId = rec.clientId,
                    serviceId = rec.serviceId,
                    adminId = rec.adminId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    status = rec.status.ToString(),
                    clientName = rec.client.clientFirstName,
                    serviceName = rec.service.serviceName,
                    number = rec.number
                })
                .ToList();
            return result;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            context.Orders.Add(new Order
            {
                clientId = model.clientId,
                serviceId = model.serviceId,
                DateCreate = DateTime.Now,
                status = OrderStatus.Принят,
                number=model.number,
                adminId=model.adminId,
                clientName=model.clientName,
                serviceName=model.serviceName
            });
            context.SaveChanges();
        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Order element = context.Orders.FirstOrDefault(rec => rec.id == model.id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var productComponents = context.ServiceResources
                                                .Where(rec => rec.serviceId == element.serviceId);
                    foreach (var productComponent in productComponents)
                    {
                        int countOnDelivers = productComponent.count;
                        var deliverComponents = context.DeliveryResources
                                                    .Where(rec => rec.resourceId == productComponent.resourceId);
                        foreach (var stockComponent in deliverComponents)
                        {
                            if (stockComponent.count >= countOnDelivers)
                            {
                                stockComponent.count -= countOnDelivers;
                                countOnDelivers = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnDelivers -= stockComponent.count;
                                stockComponent.count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnDelivers > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                productComponent.resource.resourceName + " требуется " +
                                productComponent.count + ", не хватает " + countOnDelivers);
                        }
                    }
                    element.adminId = model.adminId;
                    element.status = OrderStatus.Выполняетcя;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishOrder(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = OrderStatus.Готов;
            context.SaveChanges();
        }

        public void PayOrder(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = OrderStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutComponent(DeliveryResourceBindingModel model)
        {
            DeliveryResource element = context.DeliveryResources
                                                .FirstOrDefault(rec => rec.deliveryId == model.deliveryId &&
                                                                    rec.resourceId == model.resourceId);
            if (element != null)
            {
                element.count += model.count;
            }
            else
            {
                context.DeliveryResources.Add(new DeliveryResource
                {
                    deliveryId = model.deliveryId,
                    resourceId = model.resourceId,
                    count = model.count
                });
            }
            context.SaveChanges();
        }
    }
}
