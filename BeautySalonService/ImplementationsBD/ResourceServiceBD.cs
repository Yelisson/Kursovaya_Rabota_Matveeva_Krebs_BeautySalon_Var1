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
   public class ResourceServiceBD:IResourceService
    {
        private AbstractDataBaseContext context;

        public ResourceServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public List<ResourceViewModel> GetList()
        {
            List<ResourceViewModel> result = context.Resources
                .Select(rec => new ResourceViewModel
                {
                    id = rec.id,
                    resourceName = rec.resourceName,
                    sumCount = rec.sumCount,
                    price=rec.price
 
                })
                .ToList();
            return result;
        }

        public ResourceViewModel GetElement(int id)
        {
            Resource element = context.Resources.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new ResourceViewModel
                {
                    id = element.id,
                    resourceName = element.resourceName,
                    sumCount = element.sumCount,
                    price = element.price
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ResourceBindingModel model)
        {
            Resource element = context.Resources.FirstOrDefault(rec => rec.resourceName == model.resourceName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            context.Resources.Add(new Resource
            {
                resourceName = model.resourceName
            });
            context.SaveChanges();
        }

        public void UpdElement(ResourceBindingModel model)
        {
            Resource element = context.Resources.FirstOrDefault(rec =>
                                        rec.resourceName == model.resourceName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = context.Resources.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.resourceName = model.resourceName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Resource element = context.Resources.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                context.Resources.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
