using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BeautySalonService.ImplementationsBD
{
    public class MainServiceBD : IMainService
    {
        private AbstractDataBaseContext context;
        public static int iDClient;
        public static DateTime dateFrom;
        public static DateTime dateTo;
        public static string mailClient;

        public MainServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
            dateFrom = DateTime.Now;
            dateTo = DateTime.Now;
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
                    services = rec.orderServices.Select(os => new ServiceViewModel
                    {
                        id = os.serviceId,
                        serviceName = os.service.serviceName,
                        price = os.service.price,
                        ServiceResources = os.service.serviceResources.Select(r => new ServiceResourceViewModel
                        {
                            id = r.id,
                            resourceId = r.resourceId,
                            resourceName = r.resource.resourceName,
                        }).ToList()
                    }).ToList(),
                    number = rec.number
                })
                .ToList();

            return result;
        }

        public int GetIdClient(string fn, string sn, string p)
        {
            try
            {
                return context.Clients.First(rec => string.Equals(rec.clientFirstName, fn) && string.Equals(rec.clientSecondName, sn)
                    && string.Equals(rec.password, p)).id;
            }
            catch (Exception x)
            {
                return -1;
            }
        }

        public string getClientMail(int id)
        {
            try
            {
                return context.Clients.First(rec => rec.id == id).mail;
            }
            catch (Exception x)
            {
                return null;
            }
        }

        public List<OrderViewModel> GetListForClient(int id)
        {
            List<OrderViewModel> orders = new List<OrderViewModel>(GetList());
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                if (order.clientId == iDClient)
                {
                    string str = "";
                    foreach (var ser in order.services)
                    {
                        str += ser.serviceName + "; ";
                    }
                    result.Add(new OrderViewModel
                    {
                        id = order.id,
                        status = order.status,
                        serviceList = str,
                        DateCreate = order.DateCreate
                    });
                }
            }
            return result;

        }

        public void CreateOrder(OrderBindingModel model)
        {
            Order order = new Order
            {
                clientId = model.clientId,
                DateCreate = DateTime.Now,
                status = OrderStatus.Принят,
                number = model.number,
            };
            context.Orders.Add(order);
            context.SaveChanges();
            foreach (ServiceBindingModel ser in model.services)
            {
                context.OrderServices.Add(new OrderService
                {
                    orderId = order.id,
                    serviceId = ser.id,
                    count = ser.count
                });
            };
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

        public List<OrderViewModel> getMagic()
        {
            List<OrderViewModel> o = context.Orders
                .Select(rec => new OrderViewModel
                {
                    id = rec.id,
                    clientId = rec.clientId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    status = rec.status.ToString(),
                    clientName = rec.client.clientFirstName,
                    services = rec.orderServices.Select(os => new ServiceViewModel
                    {
                        serviceName = os.service.serviceName
                    }).ToList()
                }).ToList();
            List<OrderViewModel> result = new List<OrderViewModel>();
            int ind = 0;
            foreach (var or in o)
            {
                String str = "";
                foreach (var s in or.services)
                {
                    str += s.serviceName + "; ";
                }
                result.Add(new OrderViewModel
                {
                    id = or.id,
                    clientName = or.clientName,
                    DateCreate = or.DateCreate,
                    serviceList = str,
                    status = or.status

                });
            }
            return result;
        }

    }


}
