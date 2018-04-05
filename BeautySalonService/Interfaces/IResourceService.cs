using BeautySalonService.BindingModels;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.Interfaces
{
   public interface IResourceService
    {
        List<ResourceViewModel> GetList();
        ResourceViewModel GetElement(int id);
        void AddElement(ResourceBindingModel model);
        void UpdElement(ResourceBindingModel model);
        void DelElement(int id);
    }
}
