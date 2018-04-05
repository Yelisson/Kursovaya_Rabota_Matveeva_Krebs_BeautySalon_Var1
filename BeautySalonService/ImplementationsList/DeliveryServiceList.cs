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
   public class DeliveryServiceList:IDeliveryService
    {
        private DataListSingleton source;

        public DeliveryServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<DeliveryViewModel> GetList()
        {
            List<DeliveryViewModel> result = source.Deliverys
                .Select(rec => new DeliveryViewModel
                {
                    id = rec.id,
                    Date=rec.Date,
                    deliveryResource = source.DeliveryResources
                            .Where(recPC => recPC.deliveryId == rec.id)
                            .Select(recPC => new DeliveryResourceViewModel
                            {
                                id = recPC.id,
                                deliveryId = recPC.deliveryId,
                                resourceId = recPC.resourceId,
                                resourceName = source.Resources
                                    .FirstOrDefault(recC => recC.id == recPC.resourceId)?.resourceName,
                                count = recPC.count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public DeliveryViewModel GetElement(int id)
        {
            Delivery element = source.Deliverys.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new DeliveryViewModel
                {
                    id = element.id,
                    Date=element.Date,
                    deliveryResource = source.DeliveryResources
                            .Where(recPC => recPC.deliveryId == element.id)
                            .Select(recPC => new DeliveryResourceViewModel
                            {
                                id = recPC.id,
                                deliveryId = recPC.deliveryId,
                                resourceId = recPC.resourceId,
                                resourceName = source.Resources
                                    .FirstOrDefault(recC => recC.id == recPC.resourceId)?.resourceName,
                                count = recPC.count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(DeliveryBindingModel model)
        {
            Delivery element = source.Deliverys.FirstOrDefault(rec => rec.id != model.id);
            int maxId = source.Deliverys.Count > 0 ? source.Deliverys.Max(rec => rec.id) : 0;
            source.Deliverys.Add(new Delivery
            {
                id = maxId + 1,
                Date=model.Date
            });
        }

        public void UpdElement(DeliveryBindingModel model)
        {
            Delivery element = source.Deliverys.FirstOrDefault(rec =>
                                         rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Deliverys.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Date = model.Date;
        }

        public void DelElement(int id)
        {
            Delivery element = source.Deliverys.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.DeliveryResources.RemoveAll(rec => rec.deliveryId == id);
                source.Deliverys.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
