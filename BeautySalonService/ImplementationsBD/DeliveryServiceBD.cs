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
    public class DeliveryServiceBD:IDeliveryService
    {
        private AbstractDataBaseContext context;

        public DeliveryServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public List<DeliveryViewModel> GetList()
        {
            List<DeliveryViewModel> result = context.Deliverys
                .Select(rec => new DeliveryViewModel
                {
                    id = rec.id,
                    Date=rec.Date,
                    deliveryResource = context.DeliveryResources
                            .Where(recPC => recPC.deliveryId == rec.id)
                            .Select(recPC => new DeliveryResourceViewModel
                            {
                                id = recPC.id,
                                deliveryId = recPC.deliveryId,
                                resourceId = recPC.resourceId,
                                resourceName = recPC.resource.resourceName,
                                count = recPC.count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public DeliveryViewModel GetElement(int id)
        {
            Delivery element = context.Deliverys.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new DeliveryViewModel
                {
                    id = element.id,
                    Date=element.Date,
                    deliveryResource = context.DeliveryResources
                            .Where(recPC => recPC.deliveryId == element.id)
                            .Select(recPC => new DeliveryResourceViewModel
                            {
                                id = recPC.id,
                                deliveryId = recPC.deliveryId,
                                resourceId = recPC.resourceId,
                                resourceName = recPC.resource.resourceName,
                                count = recPC.count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(DeliveryBindingModel model)
        {
            Delivery element = context.Deliverys.FirstOrDefault(rec => rec.id != model.id);
            context.Deliverys.Add(new Delivery
            {
                Date=model.Date
            });
            context.SaveChanges();
        }

        public void UpdElement(DeliveryBindingModel model)
        {
            Delivery element = context.Deliverys.FirstOrDefault(rec =>
                                        rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Deliverys.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Date = model.Date;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Delivery element = context.Deliverys.FirstOrDefault(rec => rec.id == id);
                    if (element != null)
                    {
                        context.DeliveryResources.RemoveRange(
                                            context.DeliveryResources.Where(rec => rec.deliveryId == id));
                        context.Deliverys.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
