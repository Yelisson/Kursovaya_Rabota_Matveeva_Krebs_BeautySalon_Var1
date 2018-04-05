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
   public class ResourceServiceList:IResourceService
    {
        private DataListSingleton source;

        public ResourceServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ResourceViewModel> GetList()
        {
            List<ResourceViewModel> result = source.Resources
                .Select(rec => new ResourceViewModel
                {
                    id = rec.id,
                    resourceName = rec.resourceName,
                    sumCount=rec.sumCount,
                    price=rec.price
                })
                .ToList();
            return result;
        }

        public ResourceViewModel GetElement(int id)
        {
            Resource element = source.Resources.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new ResourceViewModel
                {
                    id = element.id,
                    resourceName = element.resourceName,
                    sumCount=element.sumCount,
                    price=element.price
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ResourceBindingModel model)
        {
            Resource element = source.Resources.FirstOrDefault(rec => rec.resourceName == model.resourceName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Resources.Count > 0 ? source.Resources.Max(rec => rec.id) : 0;
            source.Resources.Add(new Resource
            {
                id = maxId + 1,
                resourceName = model.resourceName,
                sumCount=model.sumCount,
                price=model.price
            });
        }

        public void UpdElement(ResourceBindingModel model)
        {
            Resource element = source.Resources.FirstOrDefault(rec =>
                                        rec.resourceName == model.resourceName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Resources.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.resourceName = model.resourceName;
            element.sumCount = model.sumCount;
            element.price = model.price;
        }

        public void DelElement(int id)
        {
            Resource element = source.Resources.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.Resources.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
