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
   public class ServiceServiceList:IServiceService
    {
        private DataListSingleton source;

        public ServiceServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ServiceViewModel> GetList()
        {
            List<ServiceViewModel> result = source.Services
                .Select(rec => new ServiceViewModel
                {
                    id = rec.id,
                    serviceName = rec.serviceName,
                    price = rec.price,
                    ServiceResources = source.ServiceResources
                            .Where(recPC => recPC.serviceId == rec.id)
                            .Select(recPC => new ServiceResourceViewModel
                            {
                                id = recPC.id,
                                serviceId = recPC.serviceId,
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

        public ServiceViewModel GetElement(int id)
        {
            Service element = source.Services.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new ServiceViewModel
                {
                    id = element.id,
                    serviceName = element.serviceName,
                    price = element.price,
                    ServiceResources = source.ServiceResources
                            .Where(recPC => recPC.serviceId == element.id)
                            .Select(recPC => new ServiceResourceViewModel
                            {
                                id = recPC.id,
                                serviceId = recPC.serviceId,
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
        public void AddElement(ServiceBindingModel model)
        {
            Service element = source.Services.FirstOrDefault(rec => rec.serviceName == model.serviceName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Services.Count > 0 ? source.Services.Max(rec => rec.id) : 0;
            source.Services.Add(new Service
            {
                id = maxId + 1,
                serviceName = model.serviceName,
                price = model.price
            });
            int maxPCId = source.ServiceResources.Count > 0 ?
                                    source.ServiceResources.Max(rec => rec.id) : 0;
            var groupComponents = model.serviceResources
                                        .GroupBy(rec => rec.serviceId)
                                        .Select(rec => new
                                        {
                                            resourceId = rec.Key,
                                            Count = rec.Sum(r => r.count)
                                        });
            foreach (var groupComponent in groupComponents)
            {
                source.ServiceResources.Add(new ServiceResource
                {
                    id = ++maxPCId,
                    serviceId = maxId + 1,
                    resourceId = groupComponent.resourceId,
                    count = groupComponent.Count
                });
            }
        }

        public void UpdElement(ServiceBindingModel model)
        {
            Service element = source.Services.FirstOrDefault(rec =>
                                       rec.serviceName == model.serviceName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Services.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.serviceName = model.serviceName;
            element.price = model.price;

            int maxPCId = source.ServiceResources.Count > 0 ? source.ServiceResources.Max(rec => rec.id) : 0;
            var compIds = model.serviceResources.Select(rec => rec.resourceId).Distinct();
            var updateComponents = source.ServiceResources
                                            .Where(rec => rec.serviceId == model.id &&
                                           compIds.Contains(rec.resourceId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.count = model.serviceResources
                                                .FirstOrDefault(rec => rec.id == updateComponent.id).count;
            }
            source.ServiceResources.RemoveAll(rec => rec.serviceId == model.id &&
                                       !compIds.Contains(rec.resourceId));
            var groupComponents = model.serviceResources
                                        .Where(rec => rec.id == 0)
                                        .GroupBy(rec => rec.resourceId)
                                        .Select(rec => new
                                        {
                                            resourceId = rec.Key,
                                            Count = rec.Sum(r => r.count)
                                        });
            foreach (var groupComponent in groupComponents)
            {
                ServiceResource elementPC = source.ServiceResources
                                        .FirstOrDefault(rec => rec.serviceId == model.id &&
                                                        rec.resourceId == groupComponent.resourceId);
                if (elementPC != null)
                {
                    elementPC.count += groupComponent.Count;
                }
                else
                {
                    source.ServiceResources.Add(new ServiceResource
                    {
                        id = ++maxPCId,
                        serviceId = model.id,
                        resourceId = groupComponent.resourceId,
                        count = groupComponent.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Service element = source.Services.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.ServiceResources.RemoveAll(rec => rec.serviceId == id);
                source.Services.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}

