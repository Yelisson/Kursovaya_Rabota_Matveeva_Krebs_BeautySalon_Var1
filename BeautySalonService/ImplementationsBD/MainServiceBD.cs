using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;

namespace BeautySalonService.ImplementationsBD
{
    public class MainServiceBD:IMainService
    {
        private AbstractDataBaseContext context;
        public static int iDClient;

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
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    status = rec.status.ToString(),
                    clientName = rec.client.clientFirstName,
                    serviceId = rec.serviceId,
                    serviceName=rec.service.serviceName,
                    number = rec.number
                })
                .ToList();

            return result;
        }

        public int GetIdClient(string fn, string sn, string p)
        {
            List<ClientViewModel> result = context.Clients
               //.Where(rec => string.Equals(rec.clientFirstName, fn) && string.Equals(rec.clientSecondName, sn)
                //&& string.Equals(rec.password,p))
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
            int ind = -1;
            foreach (var client in result) {
                if (string.Equals(client.clientFirstName, fn) && string.Equals(client.clientSecondName, sn)){
                    ind = client.id;
                }
            }
            return ind;
        }
        public List<OrderViewModel> GetListForClient(int id)
        {
            List<OrderViewModel> orders = new List<OrderViewModel>(GetList());
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in orders) {
                if (order.clientId == iDClient) {
                    result.Add(order);
                }
            }
            return result;

        }

        public void CreateOrder(OrderBindingModel model)
        {
            context.Orders.Add(new Order
            {
                clientId = model.clientId,
                DateCreate = DateTime.Now,
                status = OrderStatus.Принят,
                number=model.number,
                serviceId=model.serviceId
            });
            context.SaveChanges();
        }

        public void TakeOrderInWork(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Order element = context.Orders.FirstOrDefault(rec => rec.id == id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    
                    element.status = OrderStatus.Ожидание;
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
            context.Resources.FirstOrDefault(res => res.id == model.resourceId).sumCount += model.count;
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"DbUpdateException error details - {e?.InnerException?.InnerException?.Message}");
                foreach (var eve in e.Entries)
                {
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
                }
                Console.Write(sb.ToString());
                throw;
            }
        }

    }
}
