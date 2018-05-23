using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ImplementationsBD
{
   public class ServiceServiceBD:IServiceService
    {
        private AbstractDataBaseContext context;

        public ServiceServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public List<ServiceViewModel> GetList()
        {
            List<ServiceViewModel> result = context.Services
                .Select(rec => new ServiceViewModel
                {
                    id = rec.id,
                    serviceName = rec.serviceName,
                    price = rec.price,
                    ServiceResources = context.ServiceResources
                            .Where(recPC => recPC.serviceId == rec.id)
                            .Select(recPC => new ServiceResourceViewModel
                            {
                                id = recPC.id,
                                serviceId = recPC.serviceId,
                                resourceId = recPC.resourceId,
                                resourceName = recPC.resource.resourceName,
                                count = recPC.count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public ServiceViewModel GetElement(int id)
        {
            Service element = context.Services.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new ServiceViewModel
                {
                    id = element.id,
                    serviceName = element.serviceName,
                    price = element.price,
                    ServiceResources = context.ServiceResources
                            .Where(recPC => recPC.serviceId == element.id)
                            .Select(recPC => new ServiceResourceViewModel
                            {
                                id = recPC.id,
                                serviceId = recPC.serviceId,
                                resourceId = recPC.resourceId,
                                resourceName = recPC.resource.resourceName,
                                count = recPC.count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ServiceBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Service element = context.Services.FirstOrDefault(rec => rec.serviceName == model.serviceName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть услуга с таким названием");
                    }
                    element = new Service
                    {
                        serviceName = model.serviceName,
                        price = model.price
                    };
                    context.Services.Add(element);
                    try
                    {

                        context.SaveChanges();

                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                        {
                            Console.Write("Object: " + validationError.Entry.Entity.ToString());
                            Console.Write(" ");
                            foreach (DbValidationError err in validationError.ValidationErrors)
                            {
                                Console.Write(err.ErrorMessage + " ");
                        }
                        }
                    }
                    var groupComponents = model.serviceResources
                                                .GroupBy(rec => rec.resourceId)
                                                .Select(rec => new
                                                {
                                                    resourceId = rec.Key,
                                                    count = rec.Sum(r => r.count)
                                                });
                    foreach (var groupComponent in groupComponents)
                    {
                        context.ServiceResources.Add(new ServiceResource
                        {
                            serviceId = element.id,
                            resourceId = groupComponent.resourceId,
                            count = groupComponent.count
                        });
                        context.SaveChanges();
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

        public void UpdElement(ServiceBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Service element = context.Services.FirstOrDefault(rec =>
                                        rec.serviceName == model.serviceName && rec.id != model.id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть услуга с таким названием");
                    }
                    element = context.Services.FirstOrDefault(rec => rec.id == model.id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.serviceName = model.serviceName;
                    element.price = model.price;
                    context.SaveChanges();

                    var compIds = model.serviceResources.Select(rec => rec.resourceId).Distinct();
                    var updateComponents = context.ServiceResources
                                                    .Where(rec => rec.serviceId == model.id &&
                                                        compIds.Contains(rec.resourceId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.count = model.serviceResources
                                                        .FirstOrDefault(rec => rec.id == updateComponent.id).count;
                    }
                    context.SaveChanges();
                    context.ServiceResources.RemoveRange(
                                        context.ServiceResources.Where(rec => rec.serviceId == model.id &&
                                                                            !compIds.Contains(rec.serviceId)));
                    context.SaveChanges();
                    var groupComponents = model.serviceResources
                                                .Where(rec => rec.id == 0)
                                                .GroupBy(rec => rec.resourceId)
                                                .Select(rec => new
                                                {
                                                    resourceId = rec.Key,
                                                    count = rec.Sum(r => r.count)
                                                });
                    foreach (var groupComponent in groupComponents)
                    {
                        ServiceResource elementPC = context.ServiceResources
                                                .FirstOrDefault(rec => rec.serviceId == model.id &&
                                                                rec.resourceId == groupComponent.resourceId);
                        if (elementPC != null)
                        {
                            elementPC.count += groupComponent.count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.ServiceResources.Add(new ServiceResource
                            {
                                serviceId = model.id,
                                resourceId = groupComponent.resourceId,
                                count = groupComponent.count
                            });
                            context.SaveChanges();
                        }
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

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Service element = context.Services.FirstOrDefault(rec => rec.id == id);
                    if (element != null)
                    {
                        context.ServiceResources.RemoveRange(
                                            context.ServiceResources.Where(rec => rec.serviceId == id));
                        context.Services.Remove(element);
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
