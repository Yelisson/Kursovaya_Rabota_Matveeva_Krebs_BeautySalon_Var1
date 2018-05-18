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
                    adminId = rec.adminId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    status = rec.status.ToString(),
                    clientName = rec.client.clientFirstName,
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
                DateCreate = DateTime.Now,
                status = OrderStatus.Принят,
                number=model.number,
                adminId=model.adminId,
                clientName=model.clientName,
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
            context.SaveChanges();
        }
    }
}
